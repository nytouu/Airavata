using UnityEngine;

public class Book : InterativeObject
{
	[SerializeField]
	public string text;

	public override void Interract()
	{
		print(text);
	}
}
