using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfViewEnemy))]
public class EditorFieldOfViewEnemy : Editor
{

	void OnSceneGUI()
	{
		FieldOfViewEnemy fovScript = (FieldOfViewEnemy)target;

		Handles.color = fovScript.fovCircleColor;
		GUI.color = Color.white; //number color

		Handles.DrawWireDisc(fovScript.transform.position, Vector3.forward, fovScript.radius);
		
		//Number
		Handles.Label(fovScript.transform.position + (fovScript.radius + 0.5f) * Vector3.right, fovScript.radius.ToString("0.0")); 
		
		//Handle
		fovScript.radius = Handles.ScaleValueHandle(fovScript.radius, fovScript.transform.position + fovScript.transform.right * fovScript.radius, fovScript.transform.rotation, 4, Handles.ConeHandleCap, 1);
		
		Vector3 viewAngle1 = fovScript.DirectionFromAngle(-fovScript.transform.eulerAngles.z, -fovScript.angle/2);
		Vector3 viewAngle2 = fovScript.DirectionFromAngle(-fovScript.transform.eulerAngles.z, fovScript.angle/2);

		Handles.color = fovScript.fovAngleColor;
		Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngle1 * fovScript.radius);
		Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngle2 * fovScript.radius);

		if(fovScript.CanSeePlayer)
        {
			Handles.color = fovScript.playerSeenColor;
			Handles.DrawLine(fovScript.transform.position, fovScript.playerRef.transform.position);
		}

		
	}

}