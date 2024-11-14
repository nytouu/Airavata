using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InterativeObject : MonoBehaviour
{
	public GameObject player;
	public Camera cam;
	// Start is called before the first frame update
	void Start()
	{
	}

	virtual public void Interract()
	{
	}
}
