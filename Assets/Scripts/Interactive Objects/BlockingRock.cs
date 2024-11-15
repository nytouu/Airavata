using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlockingRock : Interactible
{
	private Animator _animator;
	[SerializeField] private List<Windmill> windmills;

	void Start()
	{
		_animator = GetComponent<Animator>();
	}
	public override void Interact()
	{
		_animator.Play("Move");
		foreach (Windmill windmill in windmills)
		{
			windmill.SetMoving(true, false);
		}

		Destroy(this);
	}
}
