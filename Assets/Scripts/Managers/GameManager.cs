using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager manages every manager in the game, it is a singleton.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance => _instance;

	private List<Manager> managers;

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy(this.gameObject);
		else
			_instance = this;

		managers = new List<Manager>(GetComponents<Manager>());

		DontDestroyOnLoad(gameObject);
	}

	public static T GetManager<T>() where T : Manager
	{
		foreach (Manager manager in _instance.managers)
		{
			if (manager.GetType() == typeof(T))
			{
				return manager as T;
			}
		}
		return null;
	}
}
