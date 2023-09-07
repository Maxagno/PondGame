using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public GameManager gameManager;
    private int clickCount = 0; // Counter for the number of clicks
    public int clickLevel = 1;
    public int clickRevenue = 1;

    public int UpgradeClickLevel(int amount)
    {
        clickLevel += amount;
        clickRevenue = clickLevel + clickLevel * (clickCount / 1000);
        return clickLevel;
    }

    public int getClickCount() { return clickCount; }


    public void OnClick()
    {
        clickCount++;
        gameManager.getMoney(clickRevenue);
    }
}
