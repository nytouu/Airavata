using UnityEngine;
using Cinemachine;

public class ViewBobbing : MonoBehaviour
{
	[Header("General")]
	[SerializeField]
	NoiseSettings viewBobbingNoiseProfile;
	[SerializeField]
	[Range(0.00001f, 0.005f)]
	private float transitionSpeed = 0.001f;

	[Header("Walk view bobbing")]
	[SerializeField]
	[Range(0f, 5f)]
	private float walkAmplitudeGain = 1f;
	[SerializeField]
	[Range(0f, 5f)]
	private float walkFrequencyGain = 1f;
	[Header("Sprint view bobbing")]
	[SerializeField]
	[Range(0f, 5f)]
	private float sprintAmplitudeGain = 2f;
	[SerializeField]
	[Range(0f, 5f)]
	private float sprintFrequencyGain = 1f;

	[Header("Player")]
	[SerializeField]
	private FPSController _playerController;

	private CinemachineVirtualCamera _camera;
	private CinemachineBasicMultiChannelPerlin noise;
	private InputManager _inputManager;

	// Start is called before the first frame update
	void Start()
	{
		_camera = GetComponent<CinemachineVirtualCamera>();
		_inputManager = GameManager.GetManager<InputManager>();

		noise = _camera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		noise.m_NoiseProfile = viewBobbingNoiseProfile;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 movement = _playerController.Movement;

		if ((Mathf.Abs(movement.x) > 0.01f || Mathf.Abs(movement.z) > 0.01) && _playerController.IsGrounded)
		{
			// Player is moving
			if (_inputManager.GetPlayerSprint())
			{
				noise.m_AmplitudeGain =
					Mathf.Lerp(noise.m_AmplitudeGain, sprintAmplitudeGain, Time.time * transitionSpeed);
				noise.m_FrequencyGain =
					Mathf.Lerp(noise.m_FrequencyGain, sprintFrequencyGain, Time.time * transitionSpeed);
			}
			else
			{
				noise.m_AmplitudeGain =
					Mathf.Lerp(noise.m_AmplitudeGain, walkAmplitudeGain, Time.time * transitionSpeed);
				noise.m_FrequencyGain =
					Mathf.Lerp(noise.m_FrequencyGain, walkFrequencyGain, Time.time * transitionSpeed);
			}
		}
		else
		{
			// Idle
			noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, 0f, Time.time * transitionSpeed);
			noise.m_FrequencyGain = Mathf.Lerp(noise.m_FrequencyGain, 0f, Time.time * transitionSpeed);
		}
	}
}
