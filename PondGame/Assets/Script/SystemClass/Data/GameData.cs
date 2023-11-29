using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public float cameraPositionY;

    public double money;
    public double production;
    public double moneyUsed;

    public string lastSceneName;

    public List<FishData> listOfFish = new List<FishData>();

    public ClickerData clicker = null;

    public GameData()
    {
        //Init camera position
        cameraPositionY = 0f;
        lastSceneName = "SeaLevel"; 
        // Init LevelManager data
        this.money = 0;
        this.moneyUsed = 0;
        this.production = 0;

        //Init all the fish in the corresponding levelManager

    }
}
