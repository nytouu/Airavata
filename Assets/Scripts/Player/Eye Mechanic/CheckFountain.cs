using System.Collections.Generic;
using UnityEngine;

public class CheckFountain : CheckObject
{
	[SerializeField]
	private GameObject fountain;
	void Start()
	{
		timer = 0.0f;
		objectToCheck = fountain;
		code = new List<int> { 1, 2 };
	}

	void Update()
	{
		for (int i = 0; i < codeTry.Count - 1; i++)
		{
			if (codeTry[i] == 0)
			{
				codeTry.RemoveAt(i);
			}
		}
		base.Update();
	}
}
