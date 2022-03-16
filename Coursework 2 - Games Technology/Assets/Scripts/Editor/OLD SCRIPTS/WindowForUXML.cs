using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class WindowForUXML : EditorWindow
{
    [MenuItem("UXML Window/Open Window")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(WindowForUXML));
    }

    public void OnEnable()
    {
        VisualTreeAsset theUXMLFileasset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/IasminaUXML.uxml"); //.uxml

        VisualElement theUXMLFile = theUXMLFileasset.CloneTree();

        StyleSheet theStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/IasminaUSS.uss"); //.uss

        theUXMLFile.styleSheets.Add(theStyleSheet); //here you add the style sheet to the file

        rootVisualElement.Add(theUXMLFile); //Add the UXML content to the window’s root visual element

        SetupButtons();
    }

    private void SetupButtons()
    {
        Button newButton = rootVisualElement.Q<Button>("someButtonName");
        newButton.clickable.clicked += () =>
        {
            Debug.Log("Clicked!");
        }; //this created an anonymous function that gets called when the button is clicked
    }
}
