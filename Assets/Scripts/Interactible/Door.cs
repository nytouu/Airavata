using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform door;
    [SerializeField]
    private List<ParticleSystem> particles;
    [SerializeField]
    [Range(1f, 25f)]
    private float distanceToActivate;

    [SerializeField]
    private bool particlesEnabled;

    void Start()
    {
        particlesEnabled = false;
    }

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

		// particlesEnabled is controlled by the animation clip.
		if (particlesEnabled)
		{
			foreach (ParticleSystem particle in particles)
			{
				if (!particle.isPlaying)
					particle.Play();
			}
		}
		else if (!particlesEnabled)
		{
			foreach (ParticleSystem particle in particles)
			{
				if (particle.isPlaying)
					particle.Stop();
			}
		}
    }
}
