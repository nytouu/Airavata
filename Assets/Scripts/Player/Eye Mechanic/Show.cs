using UnityEngine;

// Script à poser sur le parent de tout objet apparaissant grace au troisieme oeil
public class Show : MonoBehaviour
{
	private Eye eye;
	// Start is called before the first frame update
	void Start()
	{
		this.gameObject.SetActive(true);
	}
}
