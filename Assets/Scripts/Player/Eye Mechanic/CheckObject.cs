using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class CheckObject : MonoBehaviour
{
	[SerializeField]
	protected List<int> code;
	[SerializeField]
	public List<int> codeTry;
	public List<GameObject> codeTryObjects;
	protected GameObject objectToCheck;
	public float timer = 0f;
	public float timeLimit = 0f;
	public bool open = false;

	public UnityEvent onOpenAction;

	protected void Start()
	{
	}

	protected void Update()
	{
		if (codeTry.SequenceEqual(code))
		{
			open = true;
			onOpenAction.Invoke();
			//Destroy(objectToCheck);
			/* objectToCheck.transform.position = */
			/* 	new Vector3(objectToCheck.transform.position.x + 1.12f, objectToCheck.transform.position.y, */
			/* 				objectToCheck.transform.position.z); */
		}
	}

	public int VectorToInt(Vector2 vector)
	{
		// Joueur va droite
		if (vector == new Vector2(1, 0))
		{
			return 1;
		}
		// Joueur va gauche
		else if (vector == new Vector2(-1, 0))
		{
			return 2;
		}
		// Joueur avance
		else if (vector == new Vector2(0, 1))
		{
			return 3;
		}
		// Joueur recule
		else if (vector == new Vector2(0, -1))
		{
			return 4;
		}
		else
		{
			return 0;
		}
	}
}
