using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{
    SerializedProperty floatRVProp;

    private void OnEnable() //this acts like a the Start function
    {
        floatRVProp = serializedObject.FindProperty("floatRV");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); //this makes Unity to load the object
        EditorGUILayout.Slider(floatRVProp, 0f, 5f); //this makes the changes, in this case makes it display as a Slider
        //this uses the SerializedProperty backing field, not the object itself
        serializedObject.ApplyModifiedProperties(); //this tells Unity that the property was changed so it should be saved
    }

    //OLD OVERRIDE VOID
    //MyComponent mc = (MyComponent)target;
    //mc.floatRV = EditorGUILayout.Slider("Some label", mc.floatRV, 0f, 10f);
}
