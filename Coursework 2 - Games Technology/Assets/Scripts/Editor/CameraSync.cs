using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;


public class CameraSync : EditorWindow
{
    [SerializeField] Camera targetCam;
    [SerializeField] CameraProperties ControlCameraProperties = new CameraProperties();
    [SerializeField] float resetFieldOfView = 60f;

    [SerializeField] bool syncCam = false;

    bool resetFovOnce = false;

    //instead of making the GAMEVIEW variable public so that it can be accessed outside 
    
    //public static EditorWindow GetMainGameView() //the location of the currently active game view
    //{
    //    System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
    //    Type type = assembly.GetType("UnityEditor.GameView");
    //    EditorWindow gameview = EditorWindow.GetWindow(type);
    //    return gameview;
    //}

    public static Transform GetTargetParent() // You have to drag and drop the camera
    {
        Camera targetCam = Camera.main;
        if (targetCam != null)
        {
            targetCam = Camera.main;
            return targetCam.transform.parent; //You need the parent, not simply the camera
        }
        else
        {
            return null;
        }
    }

    static CameraSync instance = null;

    [MenuItem("Window/Camera Tool")]
    static void Init()
    {
        CameraSync window = (CameraSync)EditorWindow.GetWindow(typeof(CameraSync), false, "Cam Tool");
        window.Show();
        instance = window;
    }

    void OnEnable()
    {
        instance = this;

        SceneView.beforeSceneGui += OnPreSceneGUI; //you need this for the -> to work

        Debug.Log(Camera.main);
        targetCam = Camera.main;
    }

    static void OnPreSceneGUI(SceneView sceneView)
    {
        instance.OnPreSceneGUI(); //you need this for the lock and -> to work
    }

    void OnPreSceneGUI()
    {
        var sceneViewCam = SceneView.lastActiveSceneView.camera; //The SceneView that was most recently in focus.


            sceneViewCam.fieldOfView = ControlCameraProperties.fov; // Trick to control the scene cam fov. We need to override it every time or it will get reset by the Unity Editor


        // Update the position changes from scene view control
        
        ControlCameraProperties.Copy(sceneViewCam, GetTargetParent()); //you need this for lock and ->

        if (syncCam && targetCam != null)
        {
            ControlCameraProperties.Paste(targetCam); //this make the LOCKER work 
        }
    }

    void OnGUI()
    {
        if (SceneView.lastActiveSceneView == null) //ID
            return;

        EditorGUILayout.BeginHorizontal(); //old - puts all 5 components on the same line
        {
            //This creates the field to assign the camera, the 200 is the width
            targetCam = (Camera)EditorGUILayout.ObjectField(targetCam, typeof(Camera), true, GUILayout.MaxWidth(200), GUILayout.MaxHeight(20));

            //Space
            GUILayout.FlexibleSpace();

            //This creates the field for the Scene View Camera
            EditorGUILayout.ObjectField(SceneView.lastActiveSceneView.camera, typeof(Camera), true, GUILayout.MaxWidth(200), GUILayout.MaxHeight(20));

            //GUI.enabled = false;
            //GUI.enabled = (targetCam != null) && !syncCam;

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(); //put all 3 buttons in the same horizontal line
        {
            if (GUILayout.Button("Align Scene View with Game View")) //make the Scene align with Game
            {
                ControlCameraProperties.Copy(targetCam); //you need both of these
                SetSceneCamTransformData();
            }

            //from here below only the second button closes when pressing the LOCK
            //GUI.enabled = targetCam != null; 
            
            //This is the LOCK function, 30 is the space between the lock and ->
            syncCam = EditorGUILayout.Toggle(syncCam, "IN LockButton", GUILayout.MaxWidth(16), GUILayout.MaxHeight(20)); 

            //GUI.enabled = (targetCam != null) && !syncCam;

            if (GUILayout.Button("Align Game View with Scene view")) //make the Game align with Scene
            {
                ControlCameraProperties.Paste(targetCam);
            }

            //GUI.enabled = true;

        }
        EditorGUILayout.EndHorizontal();

        //Creates the Camera View label right before position
        EditorGUILayout.LabelField(EditorGUIUtility.ObjectContent(SceneView.lastActiveSceneView.camera, typeof(Camera)));

        //Position/ Rotation/ Fov
            GUI.changed = false;

            EditorGUIUtility.wideMode = true;

            DrawTransformData();

           
            GUILayout.BeginHorizontal();

            //Slider, assign to fov a variable
            ControlCameraProperties.fov = EditorGUILayout.Slider(new GUIContent("Field Of View:"), ControlCameraProperties.fov, 0.1f, 179f, GUILayout.ExpandWidth(true));

            //Fov Reset 
            if (GUILayout.Button("Reset", GUILayout.MaxWidth(45f))) //50 is the width of the button
            {
            ControlCameraProperties.fov = resetFieldOfView;
            }

            GUILayout.EndHorizontal();

            if (GUI.changed) //Makes the Reset button work
            {
                SetSceneCamTransformData();
                SceneView.lastActiveSceneView.Repaint(); //The Repaint method ensures that the inspector updates to show changes made in OnSceneGUI.
            }
    }

    void DrawTransformData() // POSITION AND ROTATION
    {
        ControlCameraProperties.localPosition = EditorGUILayout.Vector3Field("Position", ControlCameraProperties.localPosition);
        Vector3 newLocalRotEuler = EditorGUILayout.Vector3Field("Rotation", ControlCameraProperties.localRotEuler);

        if (newLocalRotEuler != ControlCameraProperties.localRotEuler) //This allows to drag the Rotation
        {
            ControlCameraProperties.localRotEuler = newLocalRotEuler;
            ControlCameraProperties.localRotation = Quaternion.Euler(newLocalRotEuler);
        }
    }

    void SetSceneCamTransformData()
    {
        Vector3 globalPosition = ControlCameraProperties.localPosition; //here you asign the position
        if (GetTargetParent() != null)
            globalPosition = GetTargetParent().TransformPoint(ControlCameraProperties.localPosition);

        Quaternion globalRotation = ControlCameraProperties.localRotation; //here you asign the rotation
        if (GetTargetParent() != null)
            globalRotation = GetTargetParent().transform.rotation * globalRotation;

        SetSceneCamTransformData(globalPosition, globalRotation); 
    }

    void SetSceneCamTransformData(Vector3 position, Quaternion rotation) //Parameters: position, rotation
    {
        var scene_view = SceneView.lastActiveSceneView;

        scene_view.rotation = rotation;
        scene_view.pivot = position + rotation * new Vector3(0, 0, scene_view.cameraDistance);
    }


    //NEW CLAS 

    [System.Serializable]
    private class CameraProperties
    {
        public float fov = 60f;
        public Vector3 localPosition = Vector3.zero;
        public Quaternion localRotation = Quaternion.identity;
        public Vector3 localRotEuler = Vector3.zero;

        SerializedObject serializedTargetTransform;
        SerializedProperty serializedEulerHintProp;

        public void Copy(Camera sceneCamera, Transform cameraParent)
        {
            fov = sceneCamera.fieldOfView;

            Transform targetTransform = sceneCamera.transform; //transform of the Scene camera

            Quaternion newLocalRotation;



            if (cameraParent != null)
            {
                //The local position is the camera parent's position, The Inverse Transforms position from world space to local space
                localPosition = cameraParent.InverseTransformPoint(targetTransform.position); 
                newLocalRotation = Quaternion.Inverse(cameraParent.rotation) * targetTransform.rotation;
            }
            else
            {
                //else the local position is the Scene camera's position
                localPosition = targetTransform.position;
                newLocalRotation = targetTransform.rotation;
            }


            if (localRotation != newLocalRotation) //here you update the rotation
            {
                Vector3 newLocalEuler = newLocalRotation.eulerAngles;

                localRotEuler.x += Mathf.DeltaAngle(localRotEuler.x, newLocalEuler.x);
                localRotEuler.y += Mathf.DeltaAngle(localRotEuler.y, newLocalEuler.y);
                localRotEuler.z += Mathf.DeltaAngle(localRotEuler.z, newLocalEuler.z);

                localRotation = newLocalRotation;
            }


        }

        public void Copy(Camera target) //for <- Button to work
        {
            fov = target.fieldOfView;

            Transform targetTransform = target.transform;
            localPosition = targetTransform.localPosition;

            prepareProperty(targetTransform);
            serializedTargetTransform.UpdateIfRequiredOrScript();
            localRotEuler = serializedEulerHintProp.vector3Value;

            localRotation = targetTransform.localRotation;
        }

        public void Paste(Camera target)
        {
            target.fieldOfView = fov;

            Transform targetTransform = target.transform;
            targetTransform.localPosition = localPosition;

            prepareProperty(targetTransform);
            serializedEulerHintProp.vector3Value = localRotEuler;
            serializedTargetTransform.ApplyModifiedProperties();

            targetTransform.localEulerAngles = localRotEuler;
        }

        void prepareProperty(Transform targetTransform)
        {
            if (serializedTargetTransform != null && serializedTargetTransform.targetObject == targetTransform)
                return;

            serializedTargetTransform = new SerializedObject(targetTransform);
            serializedEulerHintProp = serializedTargetTransform.FindProperty("m_LocalEulerAnglesHint");
        }
    }
}