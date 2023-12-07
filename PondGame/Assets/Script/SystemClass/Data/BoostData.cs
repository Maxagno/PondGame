using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostData : IDataPersistence
{

    [SerializeField] public string zoneId;
    [SerializeField] public string fishId;
    [SerializeField] public int id;

    [SerializeField] public double value;
    [SerializeField] public double cost_Value;

    [SerializeField] public double cost;

    [SerializeField] public string name;
    [SerializeField] public string description;

    public void LoadData(GameData data)
    {
    }
    public void SaveData(ref GameData data)
    {
    }

    public void copyData(BoostLevel boost)
    {
        this.id = boost.id;
        this.zoneId = boost.zoneId;
        this.fishId = boost.fishId;
        this.name = boost.name;
        if (boost.cost != null)
        {
            this.cost = boost.cost.amount;
        } else { this.cost = 0; }
        this.value = boost.value;
        this.cost_Value = boost.cost_Value;
        this.name = boost.name;
        this.description = boost.description;
    }

}
