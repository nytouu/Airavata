using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLook : MonoBehaviour
{
    private CharacterController _player;
    private Vector3 _playerPos = new Vector3(0, 10000, 0);
    private bool _eyeOn;
    private float _lookDistance = 1.65f;
    private CheckObject _checkObject;
    private bool _fountainActive = false;
    
    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<CharacterController>();
        _inputManager = GameManager.GetManager<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * _lookDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out var hit, _lookDistance))
        {
            //Detection CheckEye
            if (hit.transform.gameObject.TryGetComponent(typeof(CheckEye), out Component component))
            {
                if (_checkObject != null)
                {
                    _checkObject.codeTry.Clear();
                }
                if (!_eyeOn && _player.gameObject.GetComponent<PlayerOnEyePlace>().onPlace)
                {
                    _checkObject = hit.transform.GetComponent<CheckEye>();
                    _checkObject.timer += Time.deltaTime;
                    
                    if (_checkObject.timer >= _checkObject.timeLimit)
                    {
                        _checkObject.timer = 0.0f;
                        _playerPos = _player.transform.position;
                        GetComponent<Eye>().EyeActivate();
                        _eyeOn = true;
                    }
                }
            }
            //Si le regard quitte le check
            if (!hit.transform.gameObject.TryGetComponent(typeof(CheckObject), out component) && _checkObject!=null)
            {
                _checkObject.timer = 0.0f;
            }
            
            //Ajout de la valeur du checkDoor dans la liste du CheckEye
            if (hit.transform.gameObject.TryGetComponent(typeof(CheckDoor), out Component component1) && _checkObject != null)
            {
                if (_checkObject.codeTry.Count == 0 || _checkObject.codeTry[_checkObject.codeTry.Count - 1] != hit.transform.GetComponent<CheckDoor>().id)
                {
                    _checkObject.codeTry.Add(hit.transform.GetComponent<CheckDoor>().id);
                }
            }

            //Detection Fontaine
            if (hit.transform.gameObject.TryGetComponent(typeof(CheckFountain), out Component component3))
            {
                _checkObject = hit.transform.GetComponent<CheckFountain>();
                _checkObject.timer += Time.deltaTime;
                if (!_eyeOn && _player.gameObject.GetComponent<PlayerOnEyePlace>().onPlace)
                {
                    if (_checkObject.timer >= _checkObject.timeLimit)
                    {
                        GetComponent<Eye>().EyeActivate();
                        _playerPos = _player.transform.position;
                        _eyeOn = true;
                        _fountainActive = true;
                    }
                }
            }
        }
        
        //Annulation Oeil
        if(_playerPos!= new Vector3(0,10000,0) && _playerPos != _player.transform.position || _checkObject!=null && _checkObject.open)
        {
            _checkObject.timer = 0.0f; 
            _eyeOn = false;
            this.gameObject.GetComponent<Eye>().EyeDeactivate();
            _playerPos = new Vector3(0, 10000, 0);
            
            if (_checkObject != null)
            {
                _checkObject.codeTry.Clear();
                _checkObject = null;
            }

            if (_checkObject != null)
            {
                _fountainActive = false;
                _checkObject = null;
            }
        }

        //Eye Range
        if (_player.gameObject.GetComponent<PlayerOnEyePlace>().onPlace)
        {
            _lookDistance = 8f;
        }
        else
        {
            _lookDistance = 1.65f;
        }
        //Detection Input Fontaine
        if (_fountainActive && _checkObject != null)
        {
            if (Input.anyKey)
            {
                Debug.Log("pomme");
                
            }
        }
        
        //Debug.Log(_inputManager.GetPlayerMovement());
    }
}
