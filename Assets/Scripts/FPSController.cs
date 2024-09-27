using UnityEngine;

// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
	private CharacterController _playerController;

	private Vector3 _playerVelocity;
	private bool _isGrounded;
	private InputManager _inputManager;

	[SerializeField]
	private Transform mainCameraTransform;
	[SerializeField]
	private float walkSpeed = 2.0f;
	[SerializeField]
	private float sprintSpeed = 5.0f;
	[SerializeField]
	private float jumpHeight = 1.0f;
	[SerializeField]
	private float gravityValue = -9.81f;

	private void Start()
	{
		_playerController = GetComponent<CharacterController>();
		_inputManager = InputManager.Instance;
	}

	void Update()
	{
		// Ground detection using Unity's Character Controller
		_isGrounded = _playerController.isGrounded;
		if (_isGrounded && _playerVelocity.y < 0)
		{
			_playerVelocity.y = 0f;
		}

		// Player movement
		Vector2 movement = _inputManager.GetPlayerMovement();
		bool isSprinting = _inputManager.GetPlayerSprint();
		float playerSpeed = isSprinting ? sprintSpeed : walkSpeed;

		Vector3 move = new Vector3(movement.x, 0, movement.y);
		move = mainCameraTransform.forward * move.z + mainCameraTransform.right * move.x;
		move.y = 0f;
		_playerController.Move(move * Time.deltaTime * playerSpeed);

		// Jump
		if (_inputManager.GetPlayerJump() && _isGrounded)
		{
			_playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		_playerVelocity.y += gravityValue * Time.deltaTime;
		_playerController.Move(_playerVelocity * Time.deltaTime);
	}
}
