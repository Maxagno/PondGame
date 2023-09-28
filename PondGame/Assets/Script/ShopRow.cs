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

    public int fishLevel;
    public int fishPrice;
    public int fishproduction;

    public TMP_Text Name_Text;
    public TMP_Text Level_Text;
    public TMP_Text Production_Text;
    public TMP_Text Price_Text;

    public Button buyButton;

    //Public image for thumbnail

    //Instantiate

    public ShopRow(int id, int fishId, int zoneId, string fishName, string fishDescription, int fishproduction, int fishPrice)
    {
        this.id = id;
        this.fishId = fishId;
        this.zoneId = zoneId;
        this.fishName = fishName;
        this.fishDescription = fishDescription;
        this.fishproduction = fishproduction;
        this.fishPrice = fishPrice;
        fishLevel = 0;
        Name_Text.text = fishName;
        Level_Text.text = fishLevel.ToString();
        Production_Text.text = fishproduction.ToString();
        Price_Text.text = fishPrice.ToString();
    }

    public void initText()
    {
        Name_Text.text = fishName;
        Level_Text.text = fishLevel.ToString();
        Production_Text.text = fishproduction.ToString();
        Price_Text.text = fishPrice.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public int getProduction()
    {
        return fishproduction;
    }

    public int getLevel()
    {
        return fishLevel;
    }

    public int getCost()
    {
        return fishPrice;
    }

    public int getFishId()
    {
        return fishId;
    }

    public int getZoneId()
    {
        return zoneId;
    }

    public void setProduction(int production)
    {
        fishproduction = production;
    }
    
    public void setCost(int cost)
    {
        fishPrice = cost;
    }

    public void setLevel(int level)
    {
        fishLevel = level;
    }

}
