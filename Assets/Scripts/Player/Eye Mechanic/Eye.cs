using UnityEngine;
using UnityEngine.Rendering.Universal;

// Script à poser sur le player
public class Eye : MonoBehaviour
{
	private Hidden _hiddens;
	private Show _visible;
	private UniversalAdditionalCameraData _cameraData;
	private UniversalRenderPipeline _urp;
	private CharacterController _player;

	private InputManager _inputManager;
	void Start()
	{
		_hiddens = FindFirstObjectByType<Hidden>();
		_visible = FindFirstObjectByType<Show>();
		_cameraData = this.GetComponent<UniversalAdditionalCameraData>();
		_player = FindFirstObjectByType<CharacterController>();

		_inputManager = GameManager.GetManager<InputManager>();
	}
	// Update is called once per frame
	void Update()
	{
	}

	public void EyeActivate()
	{
		_cameraData.renderPostProcessing = true;
		foreach (Transform childTransform in _hiddens.transform)
		{
			if (childTransform.position.x >= _player.transform.position.x - 20 &&
				childTransform.position.x <= _player.transform.position.x + 20 &&
				childTransform.position.z >= _player.transform.position.z - 20 &&
				childTransform.position.z <= _player.transform.position.z + 20)
			{
				childTransform.gameObject.SetActive(true);
			}
		}

		foreach (Transform childTransform in _visible.transform)
		{
			if (childTransform.position.x >= _player.transform.position.x - 20 &&
				childTransform.position.x <= _player.transform.position.x + 20 &&
				childTransform.position.z >= _player.transform.position.z - 20 &&
				childTransform.position.z <= _player.transform.position.z + 20)
			{
				childTransform.gameObject.SetActive(false);
			}
		}
	}

	public void EyeDeactivate()
	{
		_cameraData.renderPostProcessing = false;
		foreach (Transform childTransform in _hiddens.transform)
		{
			childTransform.gameObject.SetActive(false);
		}

		foreach (Transform childTransform in _visible.transform)
		{
			childTransform.gameObject.SetActive(true);
		}
	}
}
