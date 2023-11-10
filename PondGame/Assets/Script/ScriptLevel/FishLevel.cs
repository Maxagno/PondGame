using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLevel : MonoBehaviour
{
    [SerializeField]
    public int id;
    public int zoneId;
    public string name;
    public AmountMoney level = new AmountMoney(0, "");

    public AmountMoney base_Production = new AmountMoney(0, "");
    public AmountMoney total_Production = new AmountMoney(0, "");


    public float boostAmount = 1f;
    public float base_boostAmount = 1f;

    public AmountMoney cost = new AmountMoney(1, "");

    public string description;

    public bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GETTER SETTER 

    public int getId() { return id; }
    public int getZoneId() {  return zoneId; }
    public string getName() { return name; }
    public AmountMoney getLevel() { return level; }
    public AmountMoney getBaseProduction() { return base_Production; }
    public AmountMoney getTotalProduction() { return total_Production; }
    public AmountMoney getCost() { return cost; }
    public string getDescription() { return description; }

    public void setLevel(AmountMoney level) {  this.level = level; }
    public void setBaseProduction(AmountMoney base_Production) { this.base_Production = base_Production; }
    public void setCost(AmountMoney cost) {  this.cost = cost; }
    
    public AmountMoney levelUp(int amount)
    {
        AmountMoney tmpProd = new AmountMoney(0, "");
        tmpProd.copyAmount(total_Production);
        level.updateAmount(amount, "");
        base_Production.updateAmount(amount, "");
        cost.updateAmount((int) ((amount * 2 + amount) * 1.5f) / 2, "");

        updateBoostPerLevel();
        updateTotalProduction();

        AmountMoney upgradeProd = new AmountMoney(0,"");
        upgradeProd.copyAmount(total_Production);
        upgradeProd.substractAllAmount(tmpProd);
        return upgradeProd;
    }


    private void updateBoostPerLevel()
    {
        float tmpAmount = boostAmount - base_boostAmount;
        int l0 = level.listGold[0];
        base_boostAmount = 1f;
        if (l0 > 9)
        {
            base_boostAmount += 0.1f;
        }
        if (l0 > 24) { base_boostAmount += 0.1f; }
        if (l0 > 49) { base_boostAmount *= 2f; }
        if (l0 > 74) { base_boostAmount += 0.2f; }
        if (l0 > 99) { base_boostAmount *= 2f; }
        if (l0 > 149) { base_boostAmount += 0.5f; }
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
            result = (float)base_Production.listGold[i] * boostAmount;
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
            total_Production.listGold[i] = (int)result % 1000;
        }
    }
}
