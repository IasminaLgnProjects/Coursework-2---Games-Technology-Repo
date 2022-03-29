using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreatePath))]
public class EditorPath : Editor
{
    CreatePath pathCreatorScript;
    Path Path
    {
        get
        {
            return pathCreatorScript.path; 
        }
    }

    void OnEnable()
    {
        pathCreatorScript = (CreatePath)target;
        if (pathCreatorScript.path == null)
        {
            pathCreatorScript.GeneratePath();
        }
    }

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        bool isClosed = GUILayout.Toggle(Path.GetIsClosed, "Closed Path");
        if (isClosed != Path.GetIsClosed)
        {
            Undo.RecordObject(pathCreatorScript, "Toggle closed path");
            Path.GetIsClosed = isClosed;
        }

        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Add new point: ");
        GUILayout.FlexibleSpace();
        GUILayout.Label("SHIFT + Left Click");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Delete point: ");
        GUILayout.FlexibleSpace();
        GUILayout.Label("Right Click on a point");
        GUILayout.EndHorizontal();

        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("2D view mode is required!");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(20f);

        if (GUILayout.Button("Reset Path (2 points)"))
        {
            Undo.RecordObject(pathCreatorScript, "Path Reset");
            pathCreatorScript.GeneratePath();
        }

    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin; //mouse position - only in 2D

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift) //if player presses Shift + click
        {
            Undo.RecordObject(pathCreatorScript, "Point added");
            Path.AddSegment(mousePos);
        }


        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
        {
            float minDistToAnchorPoint = pathCreatorScript.anchorPointDiameter; //minimum threshhold 
            int closestAnchorPointIndex = -1; //set it to an invalid index 

            for (int i = 0; i < Path.NumberOfPoints; i += 3) //incrementing by 3 just to catch the anchor points.
            {
                float dist = Vector2.Distance(mousePos, Path[i]);
                if (dist < minDistToAnchorPoint)
                {
                    minDistToAnchorPoint = dist;
                    closestAnchorPointIndex = i;
                }
            }

            if (closestAnchorPointIndex != -1)
            {
                Undo.RecordObject(pathCreatorScript, "Point deleted");
                Path.DeleteSegment(closestAnchorPointIndex);
            }
        }
    }

    void Draw()
    {
        for (int i = 0; i < Path.NumberOfPoints; i++) //creates the points 
        {
            if (i % 3 == 0 || pathCreatorScript.displayControlPoints) // % 3 moving an anchor point
            {
                //color
                if (i % 3 == 0)
                {
                    Handles.color = pathCreatorScript.anchorPointColor;
                }
                else
                {
                    Handles.color = pathCreatorScript.controlPointColor;
                }

                //Handle size
                float handleSize;
                if(i % 3 == 0)
                {
                    handleSize = pathCreatorScript.anchorPointDiameter;
                }
                else
                {
                    handleSize = pathCreatorScript.controlPointDiameter;
                }

                Vector2 newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, handleSize, Vector2.zero, Handles.SphereHandleCap); //used to be Cylinders
                if (Path[i] != newPos)
                {
                    Undo.RecordObject(pathCreatorScript, "Point moved");
                    Path.MovePoint(i, newPos);
                }

            }
        }

        for (int i = 0; i < Path.NumberOfSegments; i++) //creates the curves
        {
            Vector2[] points = Path.GetPointsInSegment(i);
            if (pathCreatorScript.displayControlPoints)
            {
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Handles.DrawBezier(points[0], points[3], points[1], points[2], pathCreatorScript.pathColor, null, 3);
        }
    }
}