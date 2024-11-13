using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using NaughtyAttributes;

public class PauseManager : Manager
{
	public const int PAUSE_LAYER = 8;

	[SerializeField]
	private Camera mainCamera;
	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private CinemachineInputProvider playerCamera;

	[SerializeField]
	private Image image;

	[SerializeField]
	private GameObject gameCrosshair;

	[Tooltip("Building that will be shown in the main menu background.")]
	[SerializeField]
	private GameObject backgroundBuildingPrefab;
	private GameObject _backgroundBuildingInstance;

	[ShowNonSerializedField]
	private static bool _isPaused;
	public static bool IsPaused => _isPaused;

	private InputManager _inputManager;
	private InputActionReference _inputAction;

	private RenderTexture _lastFrameRenderedTexture;

	private SaveLastFrame _frameSaver;

	// Start is called before the first frame update
	void Start()
	{
		_frameSaver = gameObject.AddComponent<SaveLastFrame>();
		_frameSaver.mainCamera = mainCamera;
		_frameSaver.renderTexture = _lastFrameRenderedTexture;

		_inputAction = playerCamera.XYAxis;
		_inputManager = GameManager.GetManager<InputManager>();
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
		gameCrosshair.SetActive(false);

		// Pause only after snapshot was taken.
		_frameSaver.GetLastFrame(texture => {
			ApplySavedFrameToMenuBackground(texture);

			_isPaused = true;
			Time.timeScale = 0f;

			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			playerCamera.XYAxis = null;

			_backgroundBuildingInstance = Instantiate(backgroundBuildingPrefab);
			_backgroundBuildingInstance.layer = PAUSE_LAYER;

			mainCamera.gameObject.SetActive(false);

			pauseMenu.SetActive(true);
		});
	}

	public void ResumeGame()
	{
		gameCrosshair.SetActive(true);

		_isPaused = false;
		Time.timeScale = 1f;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		playerCamera.XYAxis = _inputAction;

		Destroy(_backgroundBuildingInstance);
		mainCamera.gameObject.SetActive(true);

		pauseMenu.SetActive(false);
	}

	private void ApplySavedFrameToMenuBackground(Texture2D texture)
	{
		if (image.sprite)
			Destroy(image.sprite);
		image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
									 new Vector2(0.5f, 0.5f), 100.0f);
	}
}
