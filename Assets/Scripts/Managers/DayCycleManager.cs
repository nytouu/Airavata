using UnityEngine;

public class DayCycleManager : Manager
{
	[SerializeField]
	private float skyboxRotationSpeed = 0.5f;
	[SerializeField]
	private Light mainDirectionnalLight;
	[SerializeField]
	private float lightRotationSpeed = 0.1f;

	// Update is called once per frame
	void Update()
	{
		float deltaTime = Time.deltaTime;

		RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);

		Vector3 lightRotation = mainDirectionnalLight.transform.rotation.eulerAngles;
		mainDirectionnalLight.transform.eulerAngles =
			new Vector3(lightRotation.x, lightRotation.y + deltaTime * lightRotationSpeed, lightRotation.z);
	}
}
