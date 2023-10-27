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

    void Start()
    {
        gameManager = GameManager.GetComponent<GameManager>();
        clickRevenue = new AmountMoney(1, "");
    }


    public (ShopRow, AmountMoney) upgradeClick(int amount, ShopRow row)
    {
        clickLevel += amount;
        clickRevenue.updateAmount(clickLevel, clickRevenue.letter);
        AmountMoney cost = clickRevenue;
        return (row, cost);
    }

    public int getClickCount() { return clickCount; }

    public void OnClick()
    {
        if (!PanelManager.activeSelf)
        {
            clickCount++;
            gameManager.updateMoney(clickRevenue);
        }
        else
        {
            PanelManager.SetActive(false);
        }
    }
}
