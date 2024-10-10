using UnityEngine;

/// <summary>
/// Input manager manages the player input, it is a singleton.
/// </summary>
public class InputManager : MonoBehaviour
{
	private PlayerInput _playerInput;

	private static InputManager _instance;
	public static InputManager Instance { get => _instance; }

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy(this.gameObject);
		else
			_instance = this;

		_playerInput = new PlayerInput();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void OnEnable()
	{
		_playerInput.Enable();
	}

	private void OnDisable()
	{
		_playerInput.Disable();
	}

	/// <summary>
	/// Get player movement input
	/// </summary>
	public Vector2 GetPlayerMovement()
	{
		return _playerInput.Player.Movement.ReadValue<Vector2>();
	}

	/// <summary>
	/// Get the player look delta input
	/// </summary>
	public Vector2 GetPlayerLook()
	{
		return _playerInput.Player.Look.ReadValue<Vector2>();
	}

	/// <summary>
	/// Get player jumping input
	/// </summary>
	public bool GetPlayerJump()
	{
		return _playerInput.Player.Jump.triggered;
	}

	/// <summary>
	/// Get player jumping input
	/// </summary>
	public bool GetPlayerInteraction()
	{
		return _playerInput.Player.Interact.triggered;
	}

	/// <summary>
	/// Get player sprint input
	/// </summary>
	public bool GetPlayerSprint()
	{
		// activeControl is null while the action in waiting or cancelled
		return _playerInput.Player.Sprint.activeControl != null;
	}
}