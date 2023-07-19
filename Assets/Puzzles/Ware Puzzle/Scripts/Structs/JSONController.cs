using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Structs;

public class JSONController : MonoBehaviour
{
    [Header("Lol")]
    [SerializeField] private string type;
    [SerializeField] public int pos_x;
    [SerializeField] public int pos_y;
    [SerializeField] public int[] posibleAngles;

    [Header("Levels")]
    [SerializeField] private GridStruct[] levels;

    [Header("Save Config")]
    [SerializeField] private string savePath;
    [SerializeField] private string saveFileName = "wirePuzzle.json";

    public void SaveToFile()
    {
        LevelStruct levelCore = new LevelStruct
        {
            levels = this.levels
        };

        string json = JsonUtility.ToJson(levelCore, true);

        try
        {
            File.WriteAllText(savePath, json);
        }
        catch (Exception e)
        {
            Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
        }
    }

    public void LoadFromFile()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
            return;
        }

        try
        {
            string json = File.ReadAllText(savePath);

            //CubeStruct cubeCoreFromJson = JsonUtility.FromJson<CubeStruct>(json);
            //this.type = cubeCoreFromJson.type;
            //this.pos_x = cubeCoreFromJson.pos_x;
            //this.pos_y = cubeCoreFromJson.pos_y;
            //this.posibleAngles = cubeCoreFromJson.posibleAngles;
        }
        catch (Exception e)
        {
            Debug.Log("{GameLog} - [GameCore] - (<color=red>Error</color>) - LoadFromFile -> " + e.Message);
        }
    }

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif
        LoadFromFile();
    }

    private void OnApplicationQuit()
    {
        SaveToFile();
    }

}
