using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseDoorZone : MonoBehaviour
{
    private InputManager _inputManager;
    public UnityEvent onOpenAction;

    void Start()
    {
        _inputManager = GameManager.GetManager<InputManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_inputManager.GetPlayerSprint())
        {
            onOpenAction.Invoke();
        }
    }
}
