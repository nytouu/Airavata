using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Editor;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		/* var window = EditorWindow.GetWindow<InputManagerEditor>(); */
		if (GUILayout.Button("Edit Player Inputs")){
		
		
		}
	}
}
