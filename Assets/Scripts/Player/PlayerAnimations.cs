using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
	private enum State
	{
		Idle,
		WalkStart,
		Walk,
		WalkStrafe,
		Run,
		Jump
	}

	private Transform _orientation;
	private Animator _animator;
	private InputManager _inputManager;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_inputManager = GameManager.GetManager<InputManager>();
		_orientation = GameObject.FindWithTag("Orientation").transform;
	}

	void Update()
	{
		transform.rotation = _orientation.rotation;

		string currentAnimation = GetCurrentAnimation();

		// Player moving
		if (_inputManager.GetPlayerMovement() != Vector2.zero)
		{
			// Walking
			if (currentAnimation != State.Walk.ToString())
			{
				Play(State.WalkStart);
			}
		}
		else // Not moving
		{
			Play(State.Idle);
		}
	}

	private string GetCurrentAnimation()
	{
		return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
	}

	private void Play(State animation)
	{
		if (GetCurrentAnimation() == animation.ToString())
			return;

		_animator.Play(animation.ToString());
	}
}
