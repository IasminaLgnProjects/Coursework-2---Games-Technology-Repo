using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyComponentW4))]
public class ScaleSphere : Editor
{
    void OnSceneGUI()
    {
        MyComponentW4 mc = (MyComponentW4)target;
        Vector3 position = mc.transform.position + Vector3.up * 0.6f;
        Handles.RadiusHandle(Quaternion.identity, mc.transform.position, 1f);
    }

}
