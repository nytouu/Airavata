using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eye : MonoBehaviour
//Script à poser sur le player
{
    private List<Object> hiddens;

    void Start()
    {
        hiddens = Resources.FindObjectsOfTypeAll(typeof (Hidden)).ToList();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            foreach (Hidden obj in hiddens)
            {
                obj.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Hidden obj in hiddens)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
