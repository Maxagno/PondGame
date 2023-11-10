using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerLevel
{

    public AmountMoney level = new AmountMoney(1, "");
    public AmountMoney base_Production = new AmountMoney(1, "");
    public AmountMoney total_Production = new AmountMoney(1, "");
    
    public AmountMoney base_cost = new AmountMoney(1, "");
    public AmountMoney total_cost = new AmountMoney(1, "");

    public float boostAmount = 1f;
    public float base_boostAmount = 1f;

    public float incremental_CostUpgrade = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AmountMoney getLevel() { return level; }
    public AmountMoney getBaseProduction() { return base_Production; }
    public AmountMoney getTotalProduction() { return total_Production; }
    public AmountMoney getBaseCost() { return base_cost; }
    public AmountMoney getTotalCost() { return total_cost; }

    public void setLevel(AmountMoney level) { this.level = level; }
    public void setBaseProduction(AmountMoney base_Production) { this.base_Production = base_Production; }
    public void setBaseCost(AmountMoney cost) { this.base_cost = cost; }

    public void levelUp(int amount)
    {
        level.updateAmount(amount, "");
        base_Production.updateAmount(amount, "");
        base_cost.updateAmount(amount * 2 + amount / 2, "");
        incremental_CostUpgrade += 0.01242f * amount;
        updateBoostPerLevel();
        updateTotalProduction();
        updateTotalCost();
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

    private void updateTotalProduction()
    {
        if (boostAmount == 1f)
        {
            total_Production.copyAmount(base_Production);
            return;
        }
        float result;
        //int up;
        int down;
        for (int i = 0; i <= base_Production.index; i++)
        {
            result = (float) base_Production.listGold[i] * boostAmount;
            //up = (int) result / 1000;
            if (result >= 1000f)
            {
                total_Production.updateAmountByIndex((int)result / 1000, i + 1);
            }
            down = (int)(result * 100) % 100;
            if (down > 0 && i > 1)
            {
                total_Production.updateAmountByIndex(down, i - 1);
            }
            total_Production.listGold[i] = (int) result % 1000;
        }
    }

    private void updateTotalCost()
    {
        if (incremental_CostUpgrade == 1f)
        {
            total_cost.copyAmount(base_cost);
            return;
        }
        float result;
        //int up;
        int down;
        for (int i = 0; i <= base_cost.index; i++)
        {
            result = (float)base_cost.listGold[i] * incremental_CostUpgrade;
            //up = (int) result / 1000;
            if (result >= 1000f)
            {
                total_cost.updateAmountByIndex((int)result / 1000, i + 1);
            }
            down = (int)(result * 100) % 100;
            if (down > 0 && i > 1)
            {
                total_cost.updateAmountByIndex(down, i - 1);
            }
            total_cost.listGold[i] = (int)result % 1000;
        }
    }

}
