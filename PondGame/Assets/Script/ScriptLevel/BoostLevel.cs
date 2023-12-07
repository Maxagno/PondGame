using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostLevel : MonoBehaviour
{
    public int id;
    public string fishId;
    public string zoneId;

    public double value;
    public double cost_Value = 1.00D;

    public AmountMoney cost;

    public LevelManager levelManager;

    public string name;
    public string description;

    public TMP_Text Name_Text;
    public TMP_Text Boost_Text;
    public TMP_Text Cost_Text;

    public Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        cost = new AmountMoney(cost_Value);
        updateInfo();
    }

    public void onClickUpgrade()
    {
        if (levelManager.updateBoughtMoney(cost) == 0)
        {
            levelManager.boostUpgrade(zoneId, fishId, value/100);
            levelManager.hideRow(id);
            //double amount = fishlevel.levelUp(amountLvlUp);
            //levelManager.updateProduction(amount);


            // Need to make it disapear 
            //updateInfo();
        }
    }

    private void updateInfo()
    {
        Name_Text.text = name;
        Cost_Text.text = cost.ToString() + " G";
        Boost_Text.text = value.ToString() + "%";
    }

    public void copy(BoostLevel boost)
    {
        this.id = boost.id;
        this.zoneId = boost.zoneId;
        this.fishId = boost.fishId;
        this.levelManager = boost.levelManager;
        this.name = boost.name;
        this.cost = boost.cost;
        this.value = boost.value;
        this.cost_Value = boost.cost_Value;
        this.name = boost.name;
        this.description = boost.description;
    }
    public void loadData(BoostData boost)
    {
        this.id = boost.id;
        this.zoneId = boost.zoneId;
        this.fishId = boost.fishId;
        this.name = boost.name;
        this.cost = new AmountMoney(boost.cost);
        this.value = boost.value;
        this.cost_Value = boost.cost_Value;
        this.name = boost.name;
        this.description = boost.description;
    }

}
