using UnityEngine;

public class InputManager : Manager
{
	private PlayerInput _playerInput;

	private void Awake()
	{
		_playerInput = new PlayerInput();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void OnEnable()
	{
		_playerInput.Enable();
	}

	public void OnDisable()
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

	public bool PlayerIsMoving()
	{
		if (_playerInput.Player.Movement.ReadValue<Vector2>() == new Vector2(0, 0))
		{
			return false;
		}
		else
		{
			return true;
		}
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


	/// <summary>
	/// Get player rising input
	/// </summary>
	public float GetPlayerRise()
	{
		return _playerInput.Player.Rise.ReadValue<float>();
	}

	/// <summary>
	/// Get player rising input
	/// </summary>
	public float GetPlayerDescend()
	{
		return _playerInput.Player.Descend.ReadValue<float>();
	}
}
