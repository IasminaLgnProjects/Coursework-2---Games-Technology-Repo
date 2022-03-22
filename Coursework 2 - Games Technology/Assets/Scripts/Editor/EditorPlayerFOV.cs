using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerFOV))]
public class EditorPlayerFOV : Editor
{
    private void OnSceneGUI()
    {
        PlayerFOV playerFOVClass = (PlayerFOV)target;
        float fov = playerFOVClass.GetFov();

        Handles.color = playerFOVClass.GetColor();
        GUI.color = playerFOVClass.GetColor(); //number color

        Vector3 PlayerPosition = playerFOVClass.transform.position;
        Handles.DrawWireDisc(PlayerPosition, playerFOVClass.transform.up, fov);
        Handles.Label(PlayerPosition + (fov + 0.5f) * Vector3.right, fov.ToString("0.0"));

        // connect the handle and update via gizmo
        fov = Handles.ScaleValueHandle(fov, PlayerPosition + playerFOVClass.transform.forward * fov, playerFOVClass.transform.rotation, 5, Handles.ConeHandleCap, 1);
    }
}
