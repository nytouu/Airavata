using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckObject : MonoBehaviour
{
    [SerializeField] protected List<int> code;
    [SerializeField] public List<int> codeTry;
    protected GameObject objectToCheck;
    public float timer = 0f;
    public float timeLimit = 3.0f;
    public bool open = false;
    protected void Start()
    {
        code = new List<int> { 1, 2, 3, 1 };
    }

    protected void Update()
    {
        if (codeTry.SequenceEqual(code))
        {
            open = true;
            objectToCheck.transform.position = new Vector3(
                objectToCheck.transform.position.x,
                objectToCheck.transform.position.y + 50,
                objectToCheck.transform.position.z
            );
        }
    }
}