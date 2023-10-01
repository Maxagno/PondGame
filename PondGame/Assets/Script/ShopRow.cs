using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopRow : MonoBehaviour
{
    public int id;

    public int fishId;
    public int zoneId;

    public string fishName;
    public string fishDescription;

    public TMP_Text Name_Text;
    public TMP_Text Level_Text;
    public TMP_Text Production_Text;
    public TMP_Text Price_Text;

    public Button buyButton;

    //Public image for thumbnail

    //Instantiate

    public ShopRow(int id, int fishId, int zoneId, string fishName, string fishDescription)
    {
        this.id = id;
        this.fishId = fishId;
        this.zoneId = zoneId;
        this.fishName = fishName;
        this.fishDescription = fishDescription;
    }

    public void initText(string name, int level, int production, int price)
    {
        Name_Text.text = name;
        Level_Text.text = level.ToString();
        if (level == 0)
        {
            Production_Text.text = "0";
        }
        else
        {
            Production_Text.text = production.ToString();
        }
        Price_Text.text = price.ToString();
    }

    public void UpdateCost(int cost)
    {
        Price_Text.text = cost.ToString();
    }

    public void notEnough()
    {
        buyButton.interactable = false;
    }

    public void enough()
    {
        buyButton.interactable = true;
    }

    // GETTER SETTER

    public int getFishId()
    {
        return fishId;
    }

    public int getZoneId()
    {
        return zoneId;
    }

    public int getId()
    { return id; }

}
