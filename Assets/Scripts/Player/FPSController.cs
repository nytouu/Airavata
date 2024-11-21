using System.Linq;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
	private CharacterController _playerController;

	private float _verticalMovement;
	private InputManager _inputManager;
	private Transform _orientation;
	private Vector3 _movement;
	private bool _isGrounded;
	private bool _isSliding;
	private Vector3 _slidingVelocity;

	public bool IsGrounded => _isGrounded;
	public Vector3 Movement => new Vector3(_movement.x, _verticalMovement, _movement.z);

	[SerializeField]
	private Transform mainCameraTransform;
	[SerializeField]
	[Range(0.1f, 10f)]
	private float walkSpeed = 2.0f;
	[SerializeField]
	[Range(2f, 20f)]
	private float sprintSpeed = 5.0f;
	[SerializeField]
	[Range(0.2f, 2f)]
	private float jumpHeight = 1.0f;
	[SerializeField]
	[Range(-4f, -20f)]
	private float gravityValue = -9.81f;

	private void Start()
	{
		_playerController = GetComponent<CharacterController>();
		_inputManager = GameManager.GetManager<InputManager>();
		_orientation = new GameObject("Player Orientation").transform;
	}

	void Update()
	{
		_isGrounded = _playerController.isGrounded;

		// Ground detection using Unity's Character Controller
		if (_isGrounded && _verticalMovement < 0f)
		{
			if (_slidingVelocity != Vector3.zero)
			{
				_isSliding = true;
			}

			if (_isSliding == false)
			{
				_verticalMovement = 0f;
			}
		}

		// Orientation
		_orientation.rotation = Quaternion.Euler(0, mainCameraTransform.rotation.eulerAngles.y, 0);

		// Player movement
		bool isSprinting = _inputManager.GetPlayerSprint();
		float playerSpeed = isSprinting ? sprintSpeed : walkSpeed;

		Vector2 movement = _inputManager.GetPlayerMovement();

		if (!_isSliding)
		{
			_movement = new Vector3(movement.x, 0f, movement.y);
			_movement = _orientation.forward * _movement.z + _orientation.right * _movement.x;
			_movement.y = 0f;
			_playerController.Move(_movement * Time.deltaTime * playerSpeed);
		}

		if (_isSliding)
		{
			_movement = _slidingVelocity;
			_movement.y = _verticalMovement;
			_playerController.Move(_movement * Time.deltaTime);
		}
		

		// Jump
		if (_inputManager.GetPlayerJump() && _isGrounded && _isSliding == false)
		{
			_verticalMovement += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		_verticalMovement += gravityValue * Time.deltaTime;
		
		//Slope Sliding
		//SetSlidingVelocity();

		if (_slidingVelocity == Vector3.zero)
		{
			_isSliding = false;
		}
		
		_playerController.Move(new Vector3(0f, _verticalMovement, 0f) * Time.deltaTime);

		// Test Cinematique
		if (Input.GetKey(KeyCode.Mouse1))
		{
			_inputManager.OnDisable();
		}
		else if (Input.GetMouseButtonUp(1))
		{
			_inputManager.OnEnable();
		}
	}

	private void SetSlidingVelocity()
	{
		
		// ------------------------ 1 Raycast Version ----------------------------

		
		if (Physics.Raycast(transform.position + Vector3.down*0.9f, Vector3.down, out RaycastHit groundHit, 2))
		{
			float angle = Vector3.Angle(groundHit.normal, Vector3.up);
			//Debug.Log(angle);
			
			if (angle >= _playerController.slopeLimit)
			{
				_slidingVelocity = Vector3.ProjectOnPlane(new Vector3(0f, _verticalMovement, 0f), groundHit.normal);
				return;
			}
		}
		
		// Debug Ray
		Debug.DrawRay(transform.position + Vector3.down*0.9f, Vector3.down * 2, Color.red);
		
		// -------------------------------------------------------------------------

		

		// ------------------------ 4 Raycast Version ----------------------------
		
		/*
		float offset = 5f;
		
		// Define 4 Raycasts below player
		Vector3[] raycastOrigins = {
			
			transform.position + (Vector3.down*0.9f) + (Vector3.back / offset) + (Vector3.left / offset) , 
			transform.position + (Vector3.down*0.9f) + (Vector3.back / offset) + (Vector3.right / offset),
			transform.position + (Vector3.down*0.9f) + (Vector3.forward / offset) + (Vector3.left / offset),
			transform.position + (Vector3.down*0.9f) + (Vector3.forward / offset) + (Vector3.right / offset)
			
		};

		RaycastHit closestHit = default;
		float shortestDistance = Mathf.Infinity;
		bool hitFound = false;

		// Throw Raycasts
		foreach (Vector3 origin in raycastOrigins)
		{
			if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 2))
			{
				float distance = hit.distance;
				if (distance < shortestDistance)
				{
					closestHit = hit;
					shortestDistance = distance;
					hitFound = true;
				}
			}

			// Debug Ray
			Debug.DrawRay(origin, Vector3.down * 2, Color.red);
		}

		// Résultat final
		if (hitFound)
		{
			
			Debug.Log("Raycast le plus court trouvé !");
			Debug.Log($"Position: {closestHit.point}, Distance: {shortestDistance}");
			
			
			float angle = Vector3.Angle(closestHit.normal, Vector3.up);
			//Debug.Log(angle);
			
			if (angle >= _playerController.slopeLimit)
			{
				_slidingVelocity = Vector3.ProjectOnPlane(new Vector3(0f, _verticalMovement, 0f), closestHit.normal);
				return;
			}
			
		}
		
		*/
		
		// -------------------------------------------------------------------------

		
		if (_isSliding)
		{
			_slidingVelocity -= _slidingVelocity * Time.deltaTime * 3f;

			if (_slidingVelocity.magnitude > 1)
			{
				return;
			}
		}
		
		_slidingVelocity = Vector3.zero;
		
		
	}
	
	
}
