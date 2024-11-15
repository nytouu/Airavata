using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Windmill : MonoBehaviour
{
	private Animator _animator;
	[SerializeField] private bool moveOnStart;

    // Start is called before the first frame update
    void Start()
    {
		_animator = GetComponent<Animator>();
		SetMoving(moveOnStart);
    }

	public void SetMoving(bool moving)
	{
		if (moving)
			_animator.Play("Moving");
		else
			_animator.Play("Not Moving");
	}
}
