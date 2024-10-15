using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckEye : MonoBehaviour
{
    public List<int> code;
    public List<int> codeTry;
    public GameObject door;
    public bool open = false;
    void Start()
    {
        code = new List<int> {1, 2, 3, 1};
    }

    void Update()
    {
        if(codeTry.SequenceEqual(code))
        {
            open = true;
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 50, door.transform.position.z);
        }
    }
}
