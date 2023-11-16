using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private int clickCount = 0; // Counter for the number of clicks
    public int clickLevel = 1;
    public AmountMoney clickRevenue;

    public GameObject GameManager;
    public GameObject PanelManager;
    private GameManager gameManager;
    /*
    void Start()
    {
        gameManager = GameManager.GetComponent<GameManager>();
        clickRevenue = new AmountMoney();
    }


    public (ShopRow, double) upgradeClick(int amount, ShopRow row)
    {
        clickLevel += amount;
        clickRevenue.updateAmount(clickLevel.getAmount());
        AmountMoney cost = clickRevenue;
        return (row, cost.getAmount());
    }

    public int getClickCount() { return clickCount; }

    public void OnClick()
    {
        if (!PanelManager.activeSelf)
        {
            clickCount++;
            gameManager.updateMoney(clickRevenue.getAmount());
        }
        else
        {
            PanelManager.SetActive(false);
        }
    }*/
}
