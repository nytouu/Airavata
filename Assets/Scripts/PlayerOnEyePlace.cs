using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerOnEyePlace : MonoBehaviour
{
    public bool onPlace = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(typeof(EyePlace), out Component component))
        {
            onPlace = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(typeof(EyePlace), out Component component))
        {
            onPlace = false;
        }
    }
}
