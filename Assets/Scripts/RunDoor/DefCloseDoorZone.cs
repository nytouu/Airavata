using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefCloseDoorZone : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        onOpenAction.Invoke();
        closeCollider.transform.position = closeColliderPos;
    }
}
