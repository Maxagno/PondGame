using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public static class SaveSystem 
{
    /*
    public static void SaveData (LevelManager levelManager)
    {
        JsonSerializer serializer = new JsonSerializer();

        string path = Application.persistentDataPath + "levelManager.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelManager level = new LevelManager(levelManager);

        formatter.Serialize(stream, level);
        stream.Close();
    }

    public static LevelManager LoadLevel()
    {
        string path = Application.persistentDataPath + "levelManager.json";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelManager levelManager = formatter.Deserialize(stream) as LevelManager;
            stream.Close();

            return levelManager;
        }
        else
        {
            Debug.LogError("Save file not found in " +  path);
            return null;
        }
    }*/
}
