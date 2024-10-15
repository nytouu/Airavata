using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interrative_Object : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    virtual public void Interract()
    {

    }
}
