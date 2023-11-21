using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveData (LevelManager levelManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

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
    }
}
