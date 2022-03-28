using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSwitch : EditorWindow
{
    string text = "";
    string text2 = "";
    public int index = 0;
    public string[] options = new string[] { "aaa" };

    [MenuItem("Scene/Scene Window")]
    static void DisplayWindow()
    {
        EditorWindow.GetWindow(typeof(SceneSwitch));
    }

    void OnGUI()
    {
        text = EditorGUILayout.TextField("Name of the scene: ", text);

        if(GUILayout.Button("Switch"))
        {
            Debug.Log("clicked");

            EditorSceneManager.OpenScene("Assets/Scenes/" + text + ".unity");


            //POP-UP
        }

        if(GUILayout.Button("Button2"))
        {
            Debug.Log("button2");
        }

        text2 = EditorGUILayout.TextField("Name of the scene: ", text2);
        if (GUILayout.Button("Create Button"))
        {
            GUILayout.Button("Create Button2");

            if (GUILayout.Button(text2))
            {
                Debug.Log("button2");
            }
        }
    }
}
