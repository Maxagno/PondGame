using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private int clickCount = 0; // Counter for the number of clicks
    public int clickLevel = 1;
    public int clickRevenue = 1;

    public GameObject GameManager;
    public GameObject PanelManager;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetComponent<GameManager>();
    }


    public (ShopRow, int) upgradeClick(int amount, ShopRow row)
    {
        clickLevel += amount;
        clickRevenue = clickLevel + clickLevel;
        int cost = clickRevenue * 2;
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
