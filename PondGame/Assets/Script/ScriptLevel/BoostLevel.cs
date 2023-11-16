using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostLevel : MonoBehaviour
{
    public int id;
    public int fishId;
    public int zoneId;

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
}
