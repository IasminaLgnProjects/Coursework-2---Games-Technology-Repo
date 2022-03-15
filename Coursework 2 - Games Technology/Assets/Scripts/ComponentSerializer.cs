using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ComponentSerializer 
{
    [MenuItem("Some menu/Serialize some object")]
    static void SerializeAnObject()
    {
        var gameData = new MyComponent() { floatRV = 3 };
        string jsonString = JsonUtility.ToJson(gameData);
        var saveFile = Application.persistentDataPath + "/gamedata.json";
        File.WriteAllText(saveFile, jsonString);
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
