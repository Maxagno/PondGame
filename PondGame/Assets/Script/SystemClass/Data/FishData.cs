using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishData : IDataPersistence
{

    [SerializeField] public string sceneId;
    [SerializeField] public string zoneId;
    [SerializeField] public string id;

    [SerializeField] public double level;

    [SerializeField] public double currentCost;
    [SerializeField] public double baseCost;

    [SerializeField] public double currentProduction;
    [SerializeField] public double baseProduction;

    [SerializeField] public double boostAmount = 1D;
    [SerializeField] public double base_boostAmount = 1D;

    [SerializeField] public double future_BaseBoost = 1D;
    [SerializeField] public double future_Boost = 1D;

    [SerializeField] public double incremental_CostUpgrade = 1D;
    [SerializeField] public double future_BoostCostUpgrade = 1D;

    public void LoadData(GameData data)
    {
    }
    public void SaveData(ref GameData data)
    {
    }
}
