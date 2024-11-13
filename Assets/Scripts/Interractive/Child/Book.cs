using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interrative_Object
{
    [SerializeField]
    public string text;

    public override void Interract()
    {
        print (text);
    }
}
