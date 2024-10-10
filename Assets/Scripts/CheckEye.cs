using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckEye : MonoBehaviour
{
    public List<int> code;
    public List<int> codeTry;
    public GameObject door;
    public string codestr;
    public string codeTrystr;
    void Start()
    {
        code = new List<int> {1, 2, 3, 1};
    }

    void Update()
    {
        if(codeTry.SequenceEqual(code))
        {
            print("orange");
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 50, door.transform.position.z);
        }
    }
}
