using UnityEngine;

public class Interaction : MonoBehaviour
{
	[SerializeField]
	private Camera playerCamera;

	private InputManager _inputManager;
	private RaycastHit _hit;

	[SerializeField]
	[Range(1f, 6f)]
	private float _rangeInteraction = 3f;

	private Interactible _lastObject;

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

		if (Physics.Raycast(ray, out _hit, _rangeInteraction))
		{
			Interactible collided = _hit.collider.GetComponent<Interactible>();
			
			if (collided != null)
			{
				collided.SetHighlight(true);
            	_lastObject = collided;	
			}
			else
			{
				ResetLastObject();
			}

			if (isInteracting)
			{
				_lastObject?.Interact();
				ResetLastObject();
			}
		}
		else
		{
			ResetLastObject();
		}
		
	}

	private void ResetLastObject()
	{
		_lastObject?.SetHighlight(false);
		_lastObject = null;
	}

	private void OnTriggerEnter(Collider obj)
	{
		if (obj.TryGetComponent(typeof(Knowledge), out Component connaissance))
		{
			Destroy(connaissance.gameObject);
		}
	}
}
