using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Move1m : MonoBehaviour
{
    //MOVE 1M ON THE X AXIS CTRL + M
    [MenuItem("New Menu/Move selected object 1m X axis %m", false)]
    static void Move1mX()
    {

        print("Moved on X");
        if(Selection.activeGameObject != null)
        {
            GameObject gameObj = Selection.activeGameObject;
            Undo.RecordObject(gameObj.transform, "Moved 1m X axis");         
            gameObj.transform.position = new Vector3(gameObj.transform.position.x + 1, gameObj.transform.position.y, 0f);
            //EditorUtility.SetDirty(gameObj);
        }
    }
    [MenuItem("New Menu/Move selected object 1m X axis %m", true)]
    static bool IsDisabledX()
    {
        return (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<MeshRenderer>().enabled == true);
    }
     //MOVE 1M ON THE Y AXIS CTRL + K
    [MenuItem("New Menu/Move selected object 1m Y axis %k", false)]
    static void Move1mY()
    {
        print("clicked");
        if (Selection.activeGameObject != null)
        {
            GameObject gameObj = Selection.activeGameObject;
            Undo.RecordObject(gameObj.transform, "Moved 1m Y axis");
            gameObj.transform.position = new Vector3(gameObj.transform.position.x, gameObj.transform.position.y + 1f, 0f);
        }
    }

    //ADD LAST IN THE WINDOW MENU
    [MenuItem("Window/Disable Mesh Renderer", false, 10000)]
    static void DisableMeshR()
    {
        Selection.activeGameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
    //IMPORTANT!! Selection.gameObjects has a list of currently selected game objects

    //So you dont repeat the entire thing, you can just put it in an outside function that you recall in the "static void Move1mX()" function;


//GameObject gameObj = Selection.activeGameObject;
// gameObj.transform.position = new Vector3(gameObj.transform.position.x + 1, gameObj.transform.position.y, 0f);

