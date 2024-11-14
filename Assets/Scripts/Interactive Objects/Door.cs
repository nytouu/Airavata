using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private Transform door;
	[SerializeField]
	private List<ParticleSystem> particles;

	[SerializeField]
	private bool particlesEnabled;

	void Start()
	{
		particlesEnabled = false;
	}

	public void Open()
	{
		animator.SetBool("Should Open", true);
		StartCoroutine(nameof(StartParticles));
	}

	public void Close()
	{
		animator.SetBool("Should Open", false);
		StartCoroutine(nameof(StartParticles));
	}

	private IEnumerator StartParticles()
	{
		yield return new WaitUntil(() => particlesEnabled);
		foreach (ParticleSystem particle in particles)
		{
			if (!particle.isPlaying)
				particle.Play();
		}

		yield return new WaitUntil(() => !particlesEnabled);
		foreach (ParticleSystem particle in particles)
		{
			if (particle.isPlaying)
				particle.Stop();
		}
	}
}
