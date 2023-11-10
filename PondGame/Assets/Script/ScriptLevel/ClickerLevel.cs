using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerLevel
{

    public AmountMoney level = new AmountMoney(1, "");
    
    public AmountMoney base_Production = new AmountMoney(1, "");
    public AmountMoney total_Production = new AmountMoney(1, "");
    
    public AmountMoney future_BaseProduction = new AmountMoney(1, "");
    public AmountMoney future_Production = new AmountMoney(1, "");
    
    public AmountMoney base_cost = new AmountMoney(1, "");
    public AmountMoney total_cost = new AmountMoney(1, "");
    
    public AmountMoney future_BaseCost = new AmountMoney(1, "");
    public AmountMoney future_Cost = new AmountMoney(1, "");

    public AmountMoney total_UpgradeCost = new AmountMoney(1, "");

    public float boostAmount = 1f;
    public float base_boostAmount = 1f;
    
    public float future_BaseBoost = 1f;
    public float future_Boost = 1f;

    public float incremental_CostUpgrade = 1f;
    public float future_BoostCostUpgrade = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void levelUp(int amount)
    {
        level.updateAmount(amount, "");
        base_Production.copyAmount(future_BaseProduction);
        base_cost.copyAmount(future_BaseCost);
        total_Production.copyAmount(future_Production);
        total_cost.copyAmount(future_Cost);

        boostAmount = future_Boost;
        base_boostAmount = future_BaseBoost;
        
        incremental_CostUpgrade += 0.01242f * amount;
    }

    public AmountMoney getInfoToLevel(int amount)
    {

        future_BaseCost.copyAmount(base_cost);
        future_Cost.copyAmount(total_cost);
        future_BoostCostUpgrade = incremental_CostUpgrade;

        future_BaseProduction.copyAmount(base_Production);
        future_Boost = boostAmount;
        future_BaseBoost = base_boostAmount;

        total_UpgradeCost.copyAmount(total_cost);

        for (int i = 0; i < amount; i++)
        {
            future_BaseCost.updateAmount(1, "");
            future_BoostCostUpgrade = future_BoostCostUpgrade + 0.01242f;
            updateAmountWithBoost(future_Cost, future_BaseCost, future_BoostCostUpgrade);

            total_UpgradeCost.updateAllAmount(future_Cost);


            future_BaseProduction.updateAmount(1, "");
            updateFutureBoost(1);
            updateAmountWithBoost(future_Production, future_BaseProduction, future_Boost);
        }
        return total_UpgradeCost;
    }

    // Modify it to make it being called less
    private void updateFutureBoost(int amount)
    {
        float tmpAmount = future_Boost - future_BaseBoost;
        int l0 = level.listGold[0] + amount;
        future_BaseBoost = 1f;
        if (l0 < 10)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }

        future_BaseBoost += 0.1f;
        if (l0 < 25)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost += 0.1f;
        if (l0 < 50)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost *= 2f;
        if (l0 < 75)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost += 0.2f;
        if (l0 < 100)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost *= 2f;
        if (l0 < 150)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost += 0.5f;
        future_Boost = tmpAmount + future_BaseBoost;
    }


    private void updateAmountWithBoost(AmountMoney result, AmountMoney baseAmount, float boost)
    {
        if (boost == 1f)
        {
            result.copyAmount(baseAmount);
            return;
        }
        float tmp_result;
        //int up;
        int down;
        for (int i = 0; i <= baseAmount.index; i++)
        {
            tmp_result = (float)baseAmount.listGold[i] * boost;
            //up = (int) result / 1000;
            if (tmp_result >= 1000f)
            {
                result.updateAmountByIndex((int)tmp_result / 1000, i + 1);
            }
            down = (int)(tmp_result * 100) % 100;
            if (down > 0 && i > 1)
            {
                result.updateAmountByIndex(down, i - 1);
            }
            result.listGold[i] = (int)tmp_result % 1000;
        }
    }


    // Modify it to make it being called less
    private void updateBoostPerLevel()
    {
        float tmpAmount = boostAmount - base_boostAmount;
        int l0 = level.listGold[0];
        base_boostAmount = 1f;
        if (l0 < 10)
        {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        
        base_boostAmount += 0.1f;
        if (l0 < 25) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount += 0.1f;
        if (l0 < 50) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount *= 2f;
        if (l0 < 75) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount += 0.2f;
        if (l0 < 100) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount *= 2f;
        if (l0 < 150) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount += 0.5f;
        boostAmount = tmpAmount + base_boostAmount;
    }


    // GETTER SETTER
    public AmountMoney getLevel() { return level; }
    public AmountMoney getBaseProduction() { return base_Production; }
    public AmountMoney getTotalProduction() { return total_Production; }
    public AmountMoney getBaseCost() { return base_cost; }
    public AmountMoney getTotalCost() { return total_cost; }
    public AmountMoney getfutureCost() { return future_Cost; }
    public AmountMoney getfutureProduction() { return future_Production; }

    public void setLevel(AmountMoney level) { this.level = level; }
    public void setBaseProduction(AmountMoney base_Production) { this.base_Production = base_Production; }
    public void setBaseCost(AmountMoney cost) { this.base_cost = cost; }


}
