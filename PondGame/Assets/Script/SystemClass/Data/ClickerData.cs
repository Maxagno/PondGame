using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerData
{ 
    [SerializeField] public string sceneId;

    [SerializeField] public double level;

    [SerializeField] public double currentCost;
    [SerializeField] public double baseCost;

    [SerializeField] public double currentProduction;
    [SerializeField] public double baseProduction;

    [SerializeField] public double boostAmount = 1D;
    [SerializeField] public double base_boostAmount = 1D;

    [SerializeField] public double incremental_CostUpgrade = 1D;

    public ClickerData(string sceneId, double level, double currentCost, double baseCost, double currentProduction, double baseProduction, double boostAmount, double base_boostAmount, double incremental_CostUpgrade)
    {
        this.sceneId = sceneId;
        this.level = level;
        this.currentCost = currentCost;
        this.baseCost = baseCost;
        this.currentProduction = currentProduction;
        this.baseProduction = baseProduction;
        this.boostAmount = boostAmount;
        this.base_boostAmount = base_boostAmount;
        this.incremental_CostUpgrade = incremental_CostUpgrade;
    }
}