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

    public bool isLocked = true;

    //Public image for thumbnail

    //Instantiate

    public ShopRow(int id, int fishId, int zoneId, string fishName, string fishDescription, bool isLocked = true)
    {
        this.id = id;
        this.fishId = fishId;
        this.zoneId = zoneId;
        this.fishName = fishName;
        this.fishDescription = fishDescription;
        isLocked = isLocked;
    }

    public void initText(string name, int level, AmountMoney production, AmountMoney price)
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
        Debug.Log("Updating Price text in the row : " + price.ToString());
        Price_Text.text = price.ToString();
    }

    public void UpdateCost(AmountMoney cost)
    {
        Price_Text.text = cost.ToString();
    }

    public void canNotBeBought()
    {
        buyButton.interactable = false;
    }

    public void canBeBought()
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

    public bool getIsLocked()
    {
        return isLocked;
    }

    public void setUnlocked()
    {
        isLocked = false;
    }

}
