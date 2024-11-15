using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
	private FPSController _fpsController;
	private FreeCamController _freecamController;
	private InputManager _inputManager;

	void Start()
	{
		_inputManager = GameManager.GetManager<InputManager>();
		_fpsController = GetComponent<FPSController>();
		_freecamController = GetComponent<FreeCamController>();
	}

	void Update()
	{
		if (_inputManager.GetFlyMode())
		{
			_fpsController.enabled = !_fpsController.enabled;
			_freecamController.enabled = !_freecamController.enabled;
		}
	}
}
