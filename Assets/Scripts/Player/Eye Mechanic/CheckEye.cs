using System.Collections.Generic;
using UnityEngine;

public class CheckEye : CheckObject
{
	[SerializeField]
	private GameObject door;

	void Start()
	{
		objectToCheck = door;
		code = new List<int> { 1, 2, 3, 1 };
	}

	void Update()
	{
		base.Update();
	}
}
