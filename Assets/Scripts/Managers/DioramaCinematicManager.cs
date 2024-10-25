using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;

public class DioramaCinematicManager : MonoBehaviour
{
	[Header("Cameras controls")]
	[ShowNonSerializedField] private int _currentCamera;
	[SerializeField] private bool isPlaying = true;
	[SerializeField][Range(1f, 10f)] private float travelTime = 5f;

	[Header("Cameras")]
	[SerializeField] private List<CinemachineVirtualCamera> cameras;

	[Header("Carts")]
	[SerializeField] private List<CinemachineDollyCart> carts;

	const int CAMERA_FOCUSED = 100;
	const int CAMERA_UNFOCUSED = 0;

	// Start is called before the first frame update
	void Start()
	{
		// Here we affect the last camera
		_currentCamera = Mathf.Min(cameras.Count, carts.Count);
		StartCoroutine(nameof(CameraTravelling));
	}

	private IEnumerator CameraTravelling()
	{
		while (isPlaying)
		{
			// Because we're incrementing the value here
			_currentCamera += 1;
			if (_currentCamera >= cameras.Count || _currentCamera >= carts.Count)
			{
				// Thus making the current camera start at 0
				_currentCamera = 0;
			}

			carts[_currentCamera].m_Position = 0f;
			cameras[_currentCamera].Priority = CAMERA_FOCUSED;

			yield return new WaitForSeconds(travelTime);

			cameras[_currentCamera].Priority = CAMERA_UNFOCUSED;
		}
	}
}
