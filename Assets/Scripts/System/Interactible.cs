using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactible : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor = new Color(255, 0, 70, 64);

    private List<Material> materials;
    private List<Renderer> renderers;

    // Gets all the materials from each renderer
    private void Awake()
    {
        renderers = new List<Renderer>(GetComponents<Renderer>());

        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public virtual void Interact()
    {
        Debug.LogError("Unimplemeted Interaction");
    }

    public void SetHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", highlightColor);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
