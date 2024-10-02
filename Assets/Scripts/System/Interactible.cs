using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactible : MonoBehaviour
{
	public virtual void Interact()
	{
		Debug.LogError("Unimplemeted Interaction");
	}
}
