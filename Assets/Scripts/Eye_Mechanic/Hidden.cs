using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
//Script à poser sur le parent de tout objet apparaissant grace au troisieme oeil
{
    private Eye _eye;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }
}
