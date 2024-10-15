using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLook : MonoBehaviour
{
    private CharacterController player;
    private Vector3 playerPos;
    private bool eyeOn;
    [SerializeField]
    private CheckEye checkEye;
    private float lookDistance = 1.65f;
    private CheckFountain checkFountain;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * lookDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out var hit, lookDistance))
        {

            if (hit.transform.gameObject.TryGetComponent(typeof(CheckEye), out Component component))
            {
                if (!eyeOn)
                {
                    playerPos = player.transform.position;
                    checkEye = hit.transform.GetComponent<CheckEye>();
                    this.gameObject.GetComponent<Eye>().EyeActivate();
                    eyeOn = true;
                }

                else {
                    checkEye.codeTry.Clear();
                }
            }
            if (hit.transform.gameObject.TryGetComponent(typeof(CheckDoor), out Component component1) && checkEye != null)
            {
                if (checkEye.codeTry.Count == 0 || checkEye.codeTry[checkEye.codeTry.Count - 1] != hit.transform.GetComponent<CheckDoor>().id)
                {
                    checkEye.codeTry.Add(hit.transform.GetComponent<CheckDoor>().id);
                }
            }


            if (hit.transform.gameObject.TryGetComponent(typeof(CheckFountain), out Component component3))
            {
                checkFountain = hit.transform.GetComponent<CheckFountain>();
                this.gameObject.GetComponent<Eye>().EyeActivate();
                eyeOn = true;
            }
        }

        if(playerPos!= new Vector3(0,0,0) && player.transform.position != playerPos || checkEye!=null && checkEye.open)
        {
            eyeOn = false;
            this.gameObject.GetComponent<Eye>().EyeDeactivate();
            playerPos = new Vector3(0, 10000, 0);
            checkEye.codeTry.Clear();
        }

        if (player.gameObject.GetComponent<PlayerOnFountain>().onFountain)
        {
            lookDistance = 8f;
        }
        else
        {
            lookDistance = 1.65f;
        }
    }
}
