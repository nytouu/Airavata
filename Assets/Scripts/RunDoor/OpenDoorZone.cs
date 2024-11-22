using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoorZone : MonoBehaviour
{
    public UnityEvent onOpenAction;
    public Collider closeCollider;
    private Vector3 closeColliderPos;

    void Start()
    {
        closeColliderPos = closeCollider.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        onOpenAction.Invoke();
        if (closeCollider.transform.position == closeColliderPos)
        {
            closeCollider.transform.position = new Vector3(closeCollider.transform.position.x, closeCollider.transform.position.y + 5000, closeCollider.transform.position.z);
        }
    }
}