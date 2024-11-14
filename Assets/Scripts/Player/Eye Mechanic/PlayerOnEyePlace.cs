using UnityEngine;

public class PlayerOnEyePlace : MonoBehaviour
{
	public bool onPlace = false;
	public EyePlace eyePlace;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(typeof(EyePlace), out Component component))
		{
			eyePlace = component as EyePlace;
			onPlace = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.TryGetComponent(typeof(EyePlace), out Component component))
		{
			eyePlace = null;
			onPlace = false;
		}
	}
}
