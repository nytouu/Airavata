using UnityEngine;

// Script à poser sur le parent de tout objet apparaissant grace au troisieme oeil
public class Hidden : MonoBehaviour
{
	private Eye _eye;
	// Start is called before the first frame update
	void Start()
	{
		foreach (Transform childTransform in this.transform)
		{
			childTransform.gameObject.SetActive(false);
		}
	}
}
