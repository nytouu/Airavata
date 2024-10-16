using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PauseManager : Manager
{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private CinemachineInputProvider _playerCamera;

	[Tooltip("Building that will be shown in the main menu background.")]
	[SerializeField] private GameObject backgroundBuildingPrefab;
	#nullable enable
	private GameObject? _backgroundBuildingInstance;
	#nullable disable

	[ShowOnly][SerializeField] private static bool _isPaused;
	public static bool IsPaused => _isPaused;

	private InputManager _inputManager;
	private InputActionReference _inputAction;

	// Start is called before the first frame update
	void Start()
	{
		_inputAction = _playerCamera.XYAxis;
		_inputManager = GameManager.GetManager<InputManager>();

		pauseMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (_inputManager.GetPause())
		{
			if (_isPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
	}

	public void PauseGame()
	{
		_isPaused = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		_playerCamera.XYAxis = null;

		_backgroundBuildingInstance = Instantiate(backgroundBuildingPrefab);
	}

	public void ResumeGame()
	{
		_isPaused = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		_playerCamera.XYAxis = _inputAction;

		Destroy(_backgroundBuildingInstance);
		_backgroundBuildingInstance = null;
	}
}
