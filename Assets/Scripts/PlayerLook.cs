using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLook : MonoBehaviour
{
    private CharacterController player;
    private Vector3 playerPos;
    private bool eyeOn;
    private CheckEye checkEye;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 1.5f, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 1.5f))
        {

            if (hit.transform.gameObject.TryGetComponent(typeof(CheckEye), out Component component))
            {
                if (!eyeOn)
                {
                    playerPos = player.transform.position;
                    checkEye = hit.transform.GetComponent<CheckEye>();
                    this.gameObject.GetComponent<Eye>().EyeActivate();
                    eyeOn = true;
                    print("pomme");
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
        }

        if(playerPos!= new Vector3(0,0,0) && player.transform.position != playerPos)
        {
            eyeOn = false;
            this.gameObject.GetComponent<Eye>().EyeDeactivate();
            playerPos = new Vector3(0, 10000, 0);
            checkEye.codeTry.Clear();
        }
    }
}
