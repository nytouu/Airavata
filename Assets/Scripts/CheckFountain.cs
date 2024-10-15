using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckFountain : MonoBehaviour
{
    public List<int> code;
    public List<int> codeTry;
    public GameObject fountain;
    public bool open = false;
    void Start()
    {
        code = new List<int> { 1, 2, 3, 1 };
    }

    void Update()
    {
        if (codeTry.SequenceEqual(code))
        {
            open = true;
            fountain.transform.position = new Vector3(fountain.transform.position.x, fountain.transform.position.y + 50, fountain.transform.position.z);
        }
    }
}
