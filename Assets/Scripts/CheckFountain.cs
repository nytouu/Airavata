using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckFountain : CheckObject
{
    [SerializeField] private GameObject fountain;

    void Start()
    {
        objectToCheck = fountain;
        code = new List<int> { 1, 2 };
    }

    void Update()
    {
        base.Update();
    }

    public int KeyToInt(Input key)
    {
        return 0;
    }
}
