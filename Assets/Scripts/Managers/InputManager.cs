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
        if (!PauseManager.IsPaused)
            return _playerInput.Player.Movement.ReadValue<Vector2>();
        else
            return new Vector2(0f, 0f);
    }

    /* /// <summary> */
    /* /// Get the player look delta input */
    /* /// </summary> */
    /* public Vector2 GetPlayerLook() */
    /* { */
    /* 	if (!PauseManager.IsPaused) */
    /* 		return _playerInput.Player.Look.ReadValue<Vector2>(); */
    /* 	else */
    /* 		return new Vector2(0f, 0f); */
    /* } */

    /// <summary>
    /// Get player jumping input
    /// </summary>
    public bool GetPlayerJump()
    {
        if (!PauseManager.IsPaused)
            return _playerInput.Player.Jump.triggered;
        else
            return false;
    }

    /// <summary>
    /// Get player jumping input
    /// </summary>
    public bool GetPlayerInteraction()
    {
        if (!PauseManager.IsPaused)
            return _playerInput.Player.Interact.triggered;
        else
            return false;
    }

    /// <summary>
    /// Get player sprint input
    /// </summary>
    public bool GetPlayerSprint()
    {
        if (!PauseManager.IsPaused)
            // activeControl is null while the action in waiting or cancelled
            return _playerInput.Player.Sprint.activeControl != null;
        else
            return false;
    }

    /// <summary>
    /// Get player rising input
    /// </summary>
    public float GetPlayerRise()
    {
        if (!PauseManager.IsPaused)
            return _playerInput.Player.Rise.ReadValue<float>();
        else
            return 0f;
    }

    /// <summary>
    /// Get player rising input
    /// </summary>
    public float GetPlayerDescend()
    {
        if (!PauseManager.IsPaused)
        {
            return _playerInput.Player.Descend.ReadValue<float>();
        }
        else
        {
            return 0f;
        }
    }

    /// <summary>
    /// Get pause button
    /// </summary>
    public bool GetPause()
    {
        return _playerInput.Player.Pause.triggered;
    }
}
