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

            //This loads when inserting text 
            EditorSceneManager.OpenScene("Assets/Scenes/" + text + ".unity");


            //POP-UP
            //index = EditorGUILayout.Popup(index, options);
            //EditorGUILayout.Popup(0, options);

            //Create a field that allows you to insert buttons with the names of the scenes + Delete buttons

            //SEARCH HOT TO CREATE/DELETE BUTTONS - The name of the button (or another field) will represent the name of the scene - then call the function above
        }

        if(GUILayout.Button("Button2"))
        {
            Debug.Log("button2");
        }

        text2 = EditorGUILayout.TextField("Name of the scene: ", text2);
        if (GUILayout.Button("Create Button"))
        {
            if (GUILayout.Button(text2))
            {
                Debug.Log("button2");
            }
        }
    }
}
