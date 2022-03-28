using UnityEngine;
using UnityEditor;
public class DropObjectsEditor : EditorWindow
{
    RaycastHit hit;
    float yOffset;
    int savedLayer;
    bool AlignNormals;
    Vector3 UpVector = new Vector3(0, 90, 0);
    [MenuItem("Window/Drop Object")]                                               
    static void Awake()
    {
        EditorWindow.GetWindow<DropObjectsEditor>().Show();                         
    }

    void OnGUI()
    {
        GUILayout.Label("Drop using: ", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Bottom"))
        {
            DropObjects("Bottom");
        }

        if (GUILayout.Button("Origin"))
        {
            DropObjects("Origin");
        }

        if (GUILayout.Button("Center"))
        {
            DropObjects("Center");
        }
        EditorGUILayout.EndHorizontal();
        AlignNormals = EditorGUILayout.ToggleLeft("Align Normals", AlignNormals);  
        if (AlignNormals)
        {
            EditorGUILayout.BeginHorizontal();
            UpVector = EditorGUILayout.Vector3Field("Up Vector", UpVector);          
            GUILayout.EndHorizontal();
        }
    }

    void DropObjects(string Method)
    {
        for (int i = 0; i < Selection.transforms.Length; i++)                       
        {
            GameObject go = Selection.transforms[i].gameObject;                     
            if (!go) { continue; }                                                  

            Bounds bounds = go.GetComponent<Renderer>().bounds;                     
            savedLayer = go.layer;                                                  
            go.layer = 2;                                                           

            if (Physics.Raycast(go.transform.position, -Vector3.up, out hit))       
            {
                switch (Method)                                                     
                {
                    case "Bottom":
                        yOffset = go.transform.position.y - bounds.min.y;
                        break;
                    case "Origin":
                        yOffset = 0f;
                        break;
                    case "Center":
                        yOffset = bounds.center.y - go.transform.position.y;
                        break;
                }
                if (AlignNormals)                                                   
                                                                                    
                {
                    go.transform.up = hit.normal + UpVector;
                }
                go.transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
            go.layer = savedLayer;                                                  
        }
    }
}