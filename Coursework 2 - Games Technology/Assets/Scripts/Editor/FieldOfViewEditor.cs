using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfViewOLD))]
public class FieldOfViewEditor : Editor
{
    //FieldOfViewColors ColorScript;

    /*
    FieldOfViewColors ColorScript
    {
        get
        {
            return ColorScript; //you need this instead of the simple Path path; for the Reset function to work
        }
    }*/

    private void OnSceneGUI()
    {
        FieldOfViewOLD fovScript = (FieldOfViewOLD)target;

        //handles
        Handles.color = fovScript.fovCircleColor;
        Handles.DrawWireArc(fovScript.transform.position, Vector3.up, Vector3.forward, 360, fovScript.radius);

        //lines of the view angle 
        Vector3 viewAngle01 = DirectionFromAngle(fovScript.transform.eulerAngles.y, -fovScript.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fovScript.transform.eulerAngles.y, fovScript.angle / 2);

        Handles.color = fovScript.fovAngleColor;
        Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngle01 * fovScript.radius);
        Handles.DrawLine(fovScript.transform.position, fovScript.transform.position + viewAngle02 * fovScript.radius);

        if (fovScript.canSeePlayer)
        {
            Handles.color = fovScript.playerSeenColor;
            Handles.DrawLine(fovScript.transform.position, fovScript.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}