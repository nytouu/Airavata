using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Windmill : MonoBehaviour
{
	private Animator _animator;
	[SerializeField] private bool moveOnStart;
    [SerializeField] private bool moveBlockOnStart;

    // Start is called before the first frame update
    void Start()
    {
		_animator = GetComponent<Animator>();
		SetMoving(moveOnStart, moveBlockOnStart);
    }

	public void SetMoving(bool moving, bool blockMoving)
	{
		if (moving)
            _animator.Play("Moving");
        else if (blockMoving)
            _animator.Play("Moving Blocked");
        else
            _animator.Play("Not Moving");
	}

	
}
