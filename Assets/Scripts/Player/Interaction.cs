using UnityEngine;

public class Interaction : MonoBehaviour
{
	[SerializeField]
	private Camera playerCamera;

	private InputManager _inputManager;
	private RaycastHit _hit;
	private float _rangeInteraction = 3f;

	private GameObject _lastObject;

	// Start is called before the first frame update
	void Start()
	{
		_inputManager = InputManager.Instance;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		bool isInteracting = _inputManager.GetPlayerInteraction();

		float viewportCenterX = Screen.width / 2f;
		float viewportCenterY = Screen.height / 2f;

		Ray ray = playerCamera.ScreenPointToRay(new Vector3(viewportCenterX, viewportCenterY, 0f));

		if (Physics.Raycast(ray, out _hit, _rangeInteraction))
		{
			_hit.collider.gameObject.GetComponent<Interactible>()?.SetHighlight(true);
			_lastObject = _hit.collider.gameObject;

			if (isInteracting)
			{
				_hit.collider.gameObject.GetComponent<Interactible>()?.Interact();
			}
		}
		else
		{
			_lastObject?.GetComponent<Interactible>()?.SetHighlight(false);
		}

	}
}
