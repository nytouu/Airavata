using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
//Script à poser sur le parent de tout objet apparaissant grace au troisieme oeil
{
    private Eye eye;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }
}
