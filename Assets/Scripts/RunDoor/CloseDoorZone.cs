using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseDoorZone : MonoBehaviour
{
    private InputManager _inputManager;
    public UnityEvent onOpenAction;
    public Collider closeCollider;
    private Vector3 closeColliderPos;

    void Start()
    {
        _inputManager = GameManager.GetManager<InputManager>();
        closeColliderPos = closeCollider.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_inputManager.GetPlayerSprint())
        {
            onOpenAction.Invoke();
            closeCollider.transform.position = closeColliderPos;
        }
    }
}
