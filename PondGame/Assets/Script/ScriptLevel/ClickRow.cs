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

    public TMP_Text Name_Text;
    public TMP_Text Level_Text;
    public TMP_Text Production_Text;
    public TMP_Text Cost_Text;

    public Button buyButton;

    public bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setClicker(ClickerLevel clicker)
    {
        this.clicker = clicker;
        Name_Text.text = "Click";
        updateInfo();
    }

    public void onClickUpgrade()
    {
        levelManager.updateBoughtMoney(cost);
        clicker.levelUp(1);
        updateInfo();
    }

    private void updateInfo()
    {
        cost = clicker.getTotalCost();
        production = clicker.getTotalProduction();
        level = clicker.getLevel();
        Cost_Text.text = cost.ToString();
        Production_Text.text = production.ToString();
        Level_Text.text = level.ToString();
    }

}
