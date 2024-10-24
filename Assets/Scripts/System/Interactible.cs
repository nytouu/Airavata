using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactible : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor = new Color32(70, 70, 70, 255);

	private List<Material> _materials;
	private List<Renderer> _renderers;

	// Gets all the materials from each renderer
	private void Awake()
	{
		_renderers = new List<Renderer>(GetComponents<Renderer>());

		_materials = new List<Material>();
		foreach (Renderer renderer in _renderers)
		{
			_materials.AddRange(new List<Material>(renderer.materials));
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
			foreach (var material in _materials)
			{
				material.EnableKeyword("_EMISSION");
				material.SetColor("_EmissionColor", highlightColor);
			}
		}
		else
		{
			foreach (var material in _materials)
			{
				material.DisableKeyword("_EMISSION");
			}
		}
	}
}
