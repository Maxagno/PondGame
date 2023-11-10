using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickRow : MonoBehaviour
{
    [SerializeField]
    public int id;

    public ClickerLevel clicker;
    public LevelManager levelManager;

    public AmountMoney production;
    public AmountMoney cost;
    public AmountMoney level;

    public AmountMoney tmp_production;
    public AmountMoney tmp_cost;


    public TMP_Text Name_Text;
    public TMP_Text Level_Text;
    public TMP_Text Production_Text;
    public TMP_Text Cost_Text;

    public Button buyButton;

    public int amountLvlUp = 1;

    public bool isLocked = true;


    public void setClicker(ClickerLevel clicker)
    {
        this.clicker = clicker;
        Name_Text.text = "Click";
        updateInfo();
    }

    public void onClickUpgrade()
    {
        levelManager.updateBoughtMoney(cost);
        clicker.levelUp(amountLvlUp);
        updateInfo();
    }

    public void setAmount(int amount)
    {
        amountLvlUp = amount;
        updateInfo();
    }

    private void updateInfo()
    {
        AmountMoney tmp_level = new AmountMoney(amountLvlUp, "");
        cost = clicker.getInfoToLevel(amountLvlUp);
        production = clicker.getTotalProduction();
        level = clicker.getLevel();

        tmp_production = clicker.getfutureProduction();

        Cost_Text.text = cost.ToString();
        Production_Text.text = production.ToString() + " -> " + tmp_production.ToString();
        Level_Text.text = level.ToString() +" + " + tmp_level.ToString();
    }
}
