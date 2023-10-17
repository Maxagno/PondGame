using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockRow : MonoBehaviour
{
    public int id;
    public int fishId;
    public int zoneId;

    public string name;
    public string description;
    public int price;
    public bool isLocked = false;

    public string short_description;

    public int categoryRow;
    /*
     * 0 : Unlock new Zone
     * 1 : Unlock new Fish
     * 2 : Enable a Boost 
    */

    public TMP_Text Name_Text;
    public TMP_Text ShortDescription_Text; 
    public TMP_Text Price_Text;

    public GameObject image;

    public GameObject blockObject;

    public Button buyButton;
    public GameObject zone;


    public void initUnlockRow(int id, int zoneId, string name, string description, string short_description, int price, int category)
    {
        this.id = id;
        this.zoneId = zoneId;
        this.name = name;
        this.description = description;
        this.short_description = short_description;
        this.price = price;
        this.categoryRow = category;
        initText();
        blockObject = null;
        fishId = -1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBlock(GameObject blockObject)
    {
        this.blockObject = blockObject;
    }
    public void setfishId(int fishId)
    {
        this.fishId = fishId;
    }

    public void initText()
    {
        Name_Text.text = name;
        ShortDescription_Text.text = short_description;
        Price_Text.text = price.ToString();
    }

    public void UpdateCost(int cost)
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

    // GET SET FUNCTION 

    public int getId() { return id; }
    public string getName() { return name; }
    public string getDescription() { return description; }
    public int getPrice() { return price; }

}
