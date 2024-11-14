using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlockingRock : Interactible
{
	private Animator _animator;

	void Start()
	{
		_animator = GetComponent<Animator>();
	}
	public override void Interact()
	{
		_animator.Play("Move");
		Destroy(this);
	}
}
