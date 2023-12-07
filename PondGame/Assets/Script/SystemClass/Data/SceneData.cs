using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData 
{ 
    public float cameraPositionY;

    public double money;
    public double production;
    public double moneyUsed;

    public string sceneName;

    public List<FishData> listOfFish = new List<FishData>();
    public List<BoostData> listOfBoost = new List<BoostData>();

    public ClickerData clicker = null;

    public SceneData()
    {
    this.sceneName = "";
    //Init camera position
    cameraPositionY = 0f;
    // Init LevelManager data
    this.money = 0;
    this.moneyUsed = 0;
    this.production = 0;

    //Init all the fish in the corresponding levelManager

    }

}
