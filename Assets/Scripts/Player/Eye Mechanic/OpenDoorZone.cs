using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoorZone : MonoBehaviour
{
    public UnityEvent onOpenAction;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        onOpenAction.Invoke();
    }
}