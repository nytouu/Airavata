using UnityEngine;

public class CheckEye : CheckObject
{
	[SerializeField]
	private GameObject door;

	void Start()
	{
		objectToCheck = door;
	}

	void Update()
	{
		base.Update();
	}
}
