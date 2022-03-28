using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfViewEnemy))]
public class EditorFieldOfViewEnemy : Editor
{

	void OnSceneGUI()
	{
		FieldOfViewEnemy fovScript = (FieldOfViewEnemy)target;

		Handles.color = Color.white;
		Handles.DrawWireDisc(fovScript.transform.position, Vector3.forward, fovScript.radius);
		Vector3 viewAngle1 = fovScript.DirectionFromAngle(-fovScript.transform.eulerAngles.z, -fovScript.angle/2);
		Vector3 viewAngle2 = fovScript.DirectionFromAngle(-fovScript.transform.eulerAngles.z, fovScript.angle/2);

		Handles.color = Color.yellow;
		Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngle1 * fovScript.radius);
		Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngle2 * fovScript.radius);

		if(fovScript.CanSeePlayer)
        {
			Handles.color = Color.green;
			Handles.DrawLine(fovScript.transform.position, fovScript.playerRef.transform.position);
		}
	}

}