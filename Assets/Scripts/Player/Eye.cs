using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Eye : MonoBehaviour
//Script à poser sur le player
{
    private List<Object> hiddens;
    private List<Object> visible;
    private UniversalAdditionalCameraData cameraData;
    void Start()
    {
        hiddens = Resources.FindObjectsOfTypeAll(typeof(Hidden)).ToList();
        visible = Resources.FindObjectsOfTypeAll(typeof(Show)).ToList();
        cameraData = this.GetComponent<UniversalAdditionalCameraData>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            EyeActivate();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            EyeDeactivate();
        }
    }

    public void EyeActivate()
    {
        cameraData.renderPostProcessing = true;
        foreach (Hidden item in hiddens)
        {
            item.gameObject.SetActive(true);
        }

        foreach (Show item in visible)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void EyeDeactivate()
    {
        cameraData.renderPostProcessing = false;
        foreach (Hidden item in hiddens)
        {
            item.gameObject.SetActive(false);
        }

        foreach (Show item in visible)
        {
            item.gameObject.SetActive(true);
        }
    }
}