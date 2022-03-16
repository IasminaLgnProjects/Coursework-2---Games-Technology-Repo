using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FancyWindowVerticalSliders : EditorWindow //don't change this name to something else than EditorWindow, you can only change the name of the class
{
    float sliderV1;
    float sliderV2;
    string textF1;
    string textF2;

    [MenuItem("Fancy Window/Fancy Window Item")]
    static void DisplayFancyWindow()
    {
        EditorWindow.GetWindow(typeof(FancyWindowVerticalSliders));
    }

    void OnGUI()
    {
        //GUILayout.BeginVertical();
        //GUILayout.EndVertical();
        //sliderV = GUI.VerticalSlider(new Rect(25, 25, 100, 30), sliderV, 0f, -500f); - this was found online, does not use MinHeight            

        //sliderV = GUILayout.VerticalSlider(sliderV, 0f, -500f, GUILayout.MinHeight(150f)); - YOU DO IT THIS WAY IF YOU WANT TO DUPLICATE IT EACH TIME, NOT WITH A FUNCTION

        GUILayout.BeginHorizontal(); //you create a Horizontal Scope so you can put both sliders in the same horizontal space
        sliderV1 = AlignedSliders(sliderV1); //calls the function
        sliderV2 = AlignedSliders(sliderV2);
       GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        textF1 = AlignedTextField(textF1); //calls the function
        textF2 = AlignedTextField(textF2);
        GUILayout.EndHorizontal();

        float AlignedSliders(float currentValue)
        {
            float localFloat;

            //GUILayout.BeginHorizontal(); 
            GUILayout.FlexibleSpace();

            localFloat = GUILayout.VerticalSlider(currentValue, 0f, -500f, GUILayout.MinHeight(150f)); //if you use the VERTICAL slider, you don't use the "Editor" part.

            GUILayout.FlexibleSpace(); //you need to add blank space in both sides so that is not pushed all the way to one side
            //GUILayout.EndHorizontal();

            return localFloat;
        }

        string AlignedTextField(string currentValue)
        {
            string localString = "";
            GUILayout.BeginHorizontal(); //this one is not necessary, it does not work for both of them, but its purpose it to align it to the center
            GUILayout.FlexibleSpace();
            localString = GUILayout.TextArea(currentValue, GUILayout.MinWidth(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return localString;
        }
    }
}
