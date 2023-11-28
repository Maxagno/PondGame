using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;


public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullpath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullpath))
        {
            try
            {
                //Load the serialized data
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullpath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // deserialize the data from Json back 
                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
            } 
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullpath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullpath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //Create file
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            // Serialize the game data to Json format
            string dataToStore = JsonConvert.SerializeObject(data);

            // Write the serialized data to the file
            using(FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        } catch (Exception e) 
        {
            Debug.LogError("Error occured when trying to save data to : " + fullpath + "\n" + e);
        }
    }

}
