using UnityEngine;

// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
	private CharacterController _playerController;

	private Vector3 _playerVelocity;
	private InputManager _inputManager;
	private Transform _orientation;

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
		// Ground detection using Unity's Character Controller
		bool isGrounded = _playerController.isGrounded;
		if (isGrounded && _playerVelocity.y < 0)
		{
			_playerVelocity.y = 0f;
		}

		// Orientation
		_orientation.rotation = Quaternion.Euler(0, mainCameraTransform.rotation.eulerAngles.y, 0);

		// Player movement
		Vector2 movement = _inputManager.GetPlayerMovement();
		bool isSprinting = _inputManager.GetPlayerSprint();
		float playerSpeed = isSprinting ? sprintSpeed : walkSpeed;

		Vector3 move = new Vector3(movement.x, 0, movement.y);
		move = _orientation.forward * move.z + _orientation.right * move.x;
		move.y = 0f;
		_playerController.Move(move * Time.deltaTime * playerSpeed);

		// Jump
		if (_inputManager.GetPlayerJump() && isGrounded)
		{
			_playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		_playerVelocity.y += gravityValue * Time.deltaTime;
		_playerController.Move(_playerVelocity * Time.deltaTime);
	}
}
