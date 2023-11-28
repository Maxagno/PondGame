using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerLevel
{

    public AmountMoney level = new AmountMoney(1.00D);
    
    public AmountMoney base_Production = new AmountMoney(1.00D);
    public AmountMoney total_Production = new AmountMoney(1.00D);

    public AmountMoney future_BaseProduction = new AmountMoney(1.00D);
    public AmountMoney future_Production = new AmountMoney(1.00D);

    public AmountMoney base_cost = new AmountMoney(1.00D);
    public AmountMoney total_cost = new AmountMoney(1.00D);

    public AmountMoney future_BaseCost = new AmountMoney(1.00D);
    public AmountMoney future_Cost = new AmountMoney(1.00D);

    public AmountMoney total_UpgradeCost = new AmountMoney(1.00D);

    public double boostAmount = 1D;
    public double base_boostAmount = 1D;
    
    public double future_BaseBoost = 1D;
    public double future_Boost = 1D;

    public double incremental_CostUpgrade = 1D;
    public double future_BoostCostUpgrade = 1D;

    public void levelUp(double amount)
    {
        level.updateAmount(amount);
        base_Production.amount  = future_BaseProduction.getAmount();
        base_cost.amount = future_BaseCost.getAmount();
        total_Production.amount = future_Production.getAmount();
        total_cost.amount = future_Cost.getAmount();

        boostAmount = future_Boost;
        base_boostAmount = future_BaseBoost;
        
        incremental_CostUpgrade += 0.01242D * amount;
    }

    public AmountMoney getInfoToLevel(int amount)
    {

        future_BaseCost.amount = base_cost.getAmount();
        future_Cost.amount = total_cost.getAmount();
        future_BoostCostUpgrade = incremental_CostUpgrade;

        future_BaseProduction.amount = base_Production.getAmount();
        future_Boost = boostAmount;
        future_BaseBoost = base_boostAmount;

        total_UpgradeCost.amount = total_cost.getAmount();

        for (int i = 0; i < amount; i++)
        {
            future_BaseCost.updateAmount(1D);
            future_BoostCostUpgrade = future_BoostCostUpgrade + 0.01242D;
            updateAmountWithBoost(future_Cost, future_BaseCost, future_BoostCostUpgrade);

            total_UpgradeCost.updateAmount(future_Cost.getAmount());


            future_BaseProduction.updateAmount(1D);
            updateFutureBoost(1);
            updateAmountWithBoost(future_Production, future_BaseProduction, future_Boost);
        }
        return total_UpgradeCost;
    }

    // Modify it to make it being called less
    private void updateFutureBoost(double amount)
    {
        double tmpAmount = future_Boost - future_BaseBoost;
        double l0 = level.amount + amount;
        future_BaseBoost = 1D;
        if (l0 < 10)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }

        future_BaseBoost += 0.1D;
        if (l0 < 25)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost += 0.1D;
        if (l0 < 50)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost *= 2D;
        if (l0 < 75)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost += 0.2D;
        if (l0 < 100)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost *= 2D;
        if (l0 < 150)
        {
            future_Boost = tmpAmount + future_BaseBoost;
            return;
        }
        future_BaseBoost += 0.5D;
        future_Boost = tmpAmount + future_BaseBoost;
    }


    private void updateAmountWithBoost(AmountMoney result, AmountMoney baseAmount, double boost)
    {
        if (boost == 1D)
        {
            result.amount = baseAmount.getAmount();
            return;
        }
        result.amount = (double) Mathf.Round( (float) (baseAmount.getAmount() * boost));
    }


    // Modify it to make it being called less
    private void updateBoostPerLevel()
    {
        double tmpAmount = boostAmount - base_boostAmount;
        double l0 = level.amount;
        base_boostAmount = 1D;
        if (l0 < 10)
        {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        
        base_boostAmount += 0.1D;
        if (l0 < 25) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount += 0.1D;
        if (l0 < 50) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount *= 2D;
        if (l0 < 75) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount += 0.2D;
        if (l0 < 100) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount *= 2D;
        if (l0 < 150) {
            boostAmount = tmpAmount + base_boostAmount;
            return;
        }
        base_boostAmount += 0.5D;
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
