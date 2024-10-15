using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnFountain : MonoBehaviour
{
    public bool onFountain = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(typeof(CheckFountain), out Component component))
        {
            onFountain = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(typeof(CheckFountain), out Component component))
        {
            onFountain = false;
        }
    }
}
