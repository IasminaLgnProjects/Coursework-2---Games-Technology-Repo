using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfViewPlayer))]
public class EditorFieldOfViewPlayer : Editor
{

	void OnSceneGUI()
	{
		FieldOfViewPlayer fovScript = (FieldOfViewPlayer)target;

		Handles.color = Color.white;
		Handles.DrawWireArc(fovScript.transform.position, Vector3.forward, Vector3.right, 360, fovScript.viewRadius);
		Vector3 viewAngleA = fovScript.DirFromAngle(-fovScript.viewAngle / 2, false);
		Vector3 viewAngleB = fovScript.DirFromAngle(fovScript.viewAngle / 2, false);

		Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngleA * fovScript.viewRadius);
		Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngleB * fovScript.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fovScript.visibleTargets)
		{
			Handles.DrawLine(fovScript.transform.position, visibleTarget.position);
		}
	}

}