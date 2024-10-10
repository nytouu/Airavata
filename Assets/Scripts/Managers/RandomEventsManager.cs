using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class RandomEventsManager : MonoBehaviour
{
	[SerializeField]
	private UnityEvent onEventTrigger;
	[SerializeField]
	private float updateFrequency = 5;
	[SerializeField]
	private int inverseProbability = 100;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(nameof(Clock));
	}

	IEnumerator Clock()
	{
		int luckyNumber = Random.Range(1, inverseProbability);

		if (luckyNumber == inverseProbability)
			onEventTrigger.Invoke();

		yield return new WaitForSeconds(updateFrequency);
	}
}
