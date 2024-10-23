using System;
using System.Collections;
using UnityEngine;

// https://stackoverflow.com/questions/63928572/how-do-i-stop-a-camera-from-rendering-to-a-rendertexture
public class SaveLastFrame : MonoBehaviour
{
	public Camera mainCamera;
	public RenderTexture renderTexture;

	private void Awake()
	{
		renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
		renderTexture.useMipMap = false;
		renderTexture.antiAliasing = 1;
	}

	/// <summary>
	///  Helper method to get a snapshot of a frame
	/// </summary>
	/// <param name="onFrameSaveDone">Callback that manipulates the saved snapshot as a Texture2D</param>
	public void GetLastFrame(Action<Texture2D> onFrameSaveDone)
	{
		StartCoroutine(GetFrameRoutine(onFrameSaveDone));
	}

	private IEnumerator GetFrameRoutine(Action<Texture2D> onSnapshotDone)
	{
		yield return new WaitForEndOfFrame();

		// If RenderTexture.active is set any rendering goes into this RenderTexture
		// instead of the GameView
		RenderTexture.active = renderTexture;
		mainCamera.targetTexture = renderTexture;

		// renders into the renderTexture
		mainCamera.Render();

		// Create a new Texture2D
		var result = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
		// copies the pixels into the Texture2D
		result.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
		result.Apply();

		// reset the RenderTexture.active so nothing else is rendered into our RenderTexture
		RenderTexture.active = null;
		mainCamera.targetTexture = null;

		// Invoke the callback with the resulting snapshot Texture
		onSnapshotDone?.Invoke(result);
	}
}
