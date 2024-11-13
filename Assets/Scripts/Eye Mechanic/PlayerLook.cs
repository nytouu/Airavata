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
    [SerializeField]private CheckObject _checkObject;
    private bool _fountainActive = false;
    private InputManager _inputManager;
    private PlayerOnEyePlace _playerOnEyePlace;
    public UpDownFountain UpFountain;
    public UpDownFountain DownFountain;
    private float _timer = 0.0f;
    private float _timeLimit = 1.0f;
    private int _danceValor = 0;

    private Vector3 _eyePos;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<CharacterController>();
        _inputManager = GameManager.GetManager<InputManager>();
        _playerOnEyePlace = FindObjectOfType<PlayerOnEyePlace>();

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
                if (!_eyeOn && _playerOnEyePlace.onPlace)
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
            if (hit.transform.gameObject.TryGetComponent(typeof(CheckDoor), out Component component1) && _checkObject != null && _eyeOn)
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
                if (!_eyeOn && _player.gameObject.GetComponent<PlayerOnEyePlace>().onPlace)
                {
                    GetComponent<Eye>().EyeActivate();
                    _playerPos = _player.transform.position;
                    _eyeOn = true;
                    _fountainActive = true;
                }
            }
            //Positionement et detection des box up et down
            if (_fountainActive)
            {
                UpFountain.transform.position = new Vector3(_player.transform.position.x, UpFountain.transform.position.y, _player.transform.position.z);
                DownFountain.transform.position = new Vector3(_player.transform.position.x, DownFountain.transform.position.y, _player.transform.position.z);
                if (hit.transform.gameObject.TryGetComponent(typeof(UpDownFountain), out Component component5))
                {
                    if (_checkObject.codeTry.Count == 0 
                        || _checkObject.codeTry[_checkObject.codeTry.Count - 1] != hit.transform.GetComponent<UpDownFountain>().id)
                    {
                        _checkObject.codeTry.Add(hit.transform.GetComponent<UpDownFountain>().id);
                    }
                }
            }
            else
            {
                UpFountain.transform.position = new Vector3(10000, UpFountain.transform.position.y, 10000);
                DownFountain.transform.position = new Vector3(1000, DownFountain.transform.position.y, 1000);
            }
            //Detection Pillier
            if (hit.transform.gameObject.TryGetComponent(typeof(CheckPillar), out Component component4))
            {
                _checkObject = hit.transform.GetComponent<CheckPillar>();
                _checkObject.timer += Time.deltaTime;
                if (!_eyeOn && _player.gameObject.GetComponent<PlayerOnEyePlace>().onPlace)
                {
                    if (_checkObject.timer >= _checkObject.timeLimit)
                    {
                        _checkObject.timer = 0.0f;
                        GetComponent<Eye>().EyeActivate();
                        _playerPos = _player.transform.position;
                        _eyeOn = true;
                    }
                }
            }
        }
        
        //Annulation Oeil
        if(_playerPos!= new Vector3(0,10000,0) && _playerPos != _player.transform.position || _checkObject!=null && _checkObject.open)
        {
            _checkObject.timer = 0.0f; 
            
            if (_checkObject != null &&_checkObject.GetType() == typeof(CheckFountain) || _checkObject != null &&_checkObject.GetType() == typeof(CheckPillar))
            {
                if(!_playerOnEyePlace.onPlace || _checkObject.open) 
                {
                    _checkObject.codeTry.Clear();
                    _checkObject = null;
                    _fountainActive = false;
                    _eyeOn = false;
                    this.gameObject.GetComponent<Eye>().EyeDeactivate();
                    _playerPos = new Vector3(0, 10000, 0);
                }
            }
            
            else if (_checkObject != null && _checkObject.GetType()!= typeof(CheckFountain))
            {
                _checkObject.codeTry.Clear();
                _checkObject = null;
                _fountainActive = false;
                _eyeOn = false;
                this.gameObject.GetComponent<Eye>().EyeDeactivate();
                _playerPos = new Vector3(0, 10000, 0);
            }
        }

        //Eye Range
        if (_player.gameObject.GetComponent<PlayerOnEyePlace>().onPlace)
        {
            if (_player.gameObject.GetComponent<PlayerOnEyePlace>().eyePlace.GetType() == typeof(EyePlacePillar))
            {
                _lookDistance = 50.0f;
            }
            else
            {
                _lookDistance = 8f;
            }
        }
        else
        {
            _lookDistance = 1.65f;
        }
        //Detection Input Fontaine
        if (_fountainActive && _checkObject.GetType()== typeof(CheckFountain))
        {
            if (_inputManager.PlayerIsMoving())
            {
                _timer += Time.deltaTime;
                if (_timer>= _timeLimit  && _checkObject.codeTry.Count == 0 
                    || _timer>= _timeLimit && _checkObject.codeTry[_checkObject.codeTry.Count - 1] != _checkObject.VectorToInt(_inputManager.GetPlayerMovement()))
                { 
                    _timer = 0.0f;
                    _danceValor = (_checkObject.VectorToInt(_inputManager.GetPlayerMovement()));
                }
            }
            else
            {
                _timer = 0.0f;
                if (_danceValor != 0)
                {
                    _checkObject.codeTry.Add(_danceValor);
                    _danceValor = 0;
                }
            }
        }

        if (_checkObject != null && _checkObject.GetType() == typeof(CheckEye) && _eyeOn)
        {
            _checkObject.transform.position = Vector3.MoveTowards(_checkObject.transform.position, 
                new Vector3(hit.point.x,hit.point.y, _checkObject.transform.position.z), 0.0005f);
        }
    }
}