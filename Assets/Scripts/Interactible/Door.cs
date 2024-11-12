using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private Transform player;
	[SerializeField] private Transform door;
	[SerializeField] private float distanceToActivate;

    // Update is called once per frame
    void Update()
    {
		float distance = Vector3.Distance(player.position, door.position);

		if (distance <= distanceToActivate)
		{
		    animator.SetBool("Should Open", true);
		}
		else
		{
		    animator.SetBool("Should Open", false);
		}
    }
}
