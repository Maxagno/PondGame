using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string lastSceneName;

    public Dictionary<string, SceneData> listOfScene = new Dictionary<string, SceneData>();


    public GameData()
    {
        listOfScene.Add("SeaLevel", new SceneData());

        lastSceneName = "SeaLevel"; 

        //Init all the fish in the corresponding levelManager

    }
}
