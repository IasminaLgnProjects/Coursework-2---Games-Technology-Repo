using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomWindow : EditorWindow
{
    string textRandom = "";
    float numberR;
    float valueR;
    Color colorR;

    [MenuItem("W Menu/Window item %w")]
    static void DisplayWidow()
    {
        EditorWindow.GetWindow(typeof(CustomWindow)); //the type is the name of the class
    }

    void OnGUI()
    {
        //text
        textRandom = EditorGUILayout.TextField("Text here: ", textRandom); // , instead of +
        //numbers
        numberR = EditorGUILayout.FloatField("Number here: ", numberR);
        //slider
        valueR = EditorGUILayout.Slider("Slider here: ", valueR, -10f, +10f);
        //colour
        colorR = EditorGUILayout.ColorField("Colour here: ", colorR);

        //buttons
        if (GUILayout.Button("Button"))                 //you don't need the "Editor" part from the EditorGUILayout 
            Debug.Log("clicked"); //you cannot use PRINT

        //grid buttons
        GUILayout.BeginHorizontal();
            GUILayout.Button("Button1"); 
            GUILayout.Button("Button2"); 
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Button("Button3");
            GUILayout.Button("Button4");
        GUILayout.EndHorizontal();
    }
}
