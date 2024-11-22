using UnityEngine;
using UnityEngine.Rendering;

// Script à poser sur le player
public class Eye : MonoBehaviour
{
	private Hidden _hiddens;
	private Show _visible;
	private CharacterController _player;

	[SerializeField] private Volume volume;

	private InputManager _inputManager;
	void Start()
	{
		_hiddens = FindFirstObjectByType<Hidden>();
		_visible = FindFirstObjectByType<Show>();
		_player = FindFirstObjectByType<CharacterController>();

		_inputManager = GameManager.GetManager<InputManager>();
	}
	// Update is called once per frame
	void Update()
	{
	}

	public void EyeActivate()
	{
		volume.weight = 0.5f;
		/* foreach (Transform childTransform in _hiddens.transform) */
		/* { */
		/* 	if (childTransform.position.x >= _player.transform.position.x - 10 && */
		/* 		childTransform.position.x <= _player.transform.position.x + 10 && */
		/* 		childTransform.position.z >= _player.transform.position.z - 10 && */
		/* 		childTransform.position.z <= _player.transform.position.z + 10) */
		/* 	{ */
		/* 		childTransform.gameObject.SetActive(true); */
		/* 	} */
		/* } */

		/* foreach (Transform childTransform in _visible.transform) */
		/* { */
		/* 	if (childTransform.position.x >= _player.transform.position.x - 10 && */
		/* 		childTransform.position.x <= _player.transform.position.x + 10 && */
		/* 		childTransform.position.z >= _player.transform.position.z - 10 && */
		/* 		childTransform.position.z <= _player.transform.position.z + 10) */
		/* 	{ */
		/* 		childTransform.gameObject.SetActive(false); */
		/* 	} */
		/* } */
	}

	public void EyeDeactivate()
	{
		volume.weight = 0f;
		/* foreach (Transform childTransform in _hiddens.transform) */
		/* { */
		/* 	childTransform.gameObject.SetActive(false); */
		/* } */
		/*  */
		/* foreach (Transform childTransform in _visible.transform) */
		/* { */
		/* 	childTransform.gameObject.SetActive(true); */
		/* } */
	}
}
