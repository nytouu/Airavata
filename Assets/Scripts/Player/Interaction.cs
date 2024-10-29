using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour
{
	[SerializeField]
	private Camera playerCamera;

	private InputManager _inputManager;
	private RaycastHit _hit;

	[SerializeField][Range(1f, 6f)] private float rangeInteraction = 3f;

	private GameObject _lastObject;

	// Start is called before the first frame update
	void Start()
	{
		_inputManager = GameManager.GetManager<InputManager>();
	}

	// Update is called once per frame
	void Update()
	{
		bool isInteracting = _inputManager.GetPlayerInteraction();
		float viewportCenterX = Screen.width / 2f;
		float viewportCenterY = Screen.height / 2f;

		Ray ray = playerCamera.ScreenPointToRay(new Vector3(viewportCenterX, viewportCenterY, 0f));

		if (Physics.Raycast(ray, out _hit, rangeInteraction))
		{
			_hit.collider.gameObject.GetComponent<Interactible>()?.SetHighlight(true);
			_lastObject = _hit.collider.gameObject;
			
			if (isInteracting)
			{
				_hit.collider.gameObject.GetComponent<Interactible>()?.Interact();
				_lastObject = null;
			}
		}
		else
		{
			_lastObject?.GetComponent<Interactible>()?.SetHighlight(false);
		}
	}

	private void OnTriggerEnter(Collider obj)
	{
		if (obj.TryGetComponent(typeof(Connaissance), out Component connaissance))
		{
			Destroy(connaissance.gameObject);
		}
	}
}
