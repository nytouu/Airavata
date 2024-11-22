using System.Collections.Generic;
using UnityEngine;

// cracra, devrait hériter de interactible ou implémenter une interface IHighlightable
public class CheckDoor : MonoBehaviour
{
	public int id;

	[SerializeField]
	private Color highlightColor = new Color(100, 0, 70, 64);

	private List<Material> materials;
	private List<Renderer> renderers;

	private void Awake()
	{
		renderers = new List<Renderer>(GetComponents<Renderer>());

		materials = new List<Material>();
		foreach (var renderer in renderers)
		{
			materials.AddRange(new List<Material>(renderer.materials));
		}
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
