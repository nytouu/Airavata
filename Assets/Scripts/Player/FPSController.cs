using UnityEngine;

// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
	private CharacterController _playerController;

	private Vector3 _playerVelocity;
	private InputManager _inputManager;
	private Transform _orientation;
	private Vector3 _movement;
	private bool _isGrounded;

	public bool IsGrounded => _isGrounded;
	public Vector3 Movement => new Vector3(_movement.x, _playerVelocity.y, _movement.z);

	[SerializeField] private Transform mainCameraTransform;
	[SerializeField][Range(0.1f, 10f)] private float walkSpeed = 2.0f;
	[SerializeField][Range(2f, 20f)] private float sprintSpeed = 5.0f;
	[SerializeField][Range(0.2f, 2f)] private float jumpHeight = 1.0f;
	[SerializeField][Range(-4f, -20f)] private float gravityValue = -9.81f;

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
		if (_isGrounded && _playerVelocity.y < 0f)
		{
			_playerVelocity.y = 0f;
		}

		// Orientation
		_orientation.rotation = Quaternion.Euler(0, mainCameraTransform.rotation.eulerAngles.y, 0);

		// Player movement
		Vector2 movement = _inputManager.GetPlayerMovement();
		bool isSprinting = _inputManager.GetPlayerSprint();
		float playerSpeed = isSprinting ? sprintSpeed : walkSpeed;

		_movement = new Vector3(movement.x, 0, movement.y);
		_movement = _orientation.forward * _movement.z + _orientation.right * _movement.x;
		_movement.y = 0f;
		_playerController.Move(_movement * Time.deltaTime * playerSpeed);

		// Jump
		if (_inputManager.GetPlayerJump() && _isGrounded)
		{
			_playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		_playerVelocity.y += gravityValue * Time.deltaTime;
		_playerController.Move(_playerVelocity * Time.deltaTime);
	}
}
