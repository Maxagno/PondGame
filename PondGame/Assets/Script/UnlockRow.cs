using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockRow : MonoBehaviour
{
    public int id;

    public string name;
    public string description;
    public int price;
    public bool isLocked = false;

    public string short_description;


    public TMP_Text Name_Text;
    public TMP_Text ShortDescription_Text; 
    public TMP_Text Price_Text;

    public GameObject image;

    public Button buyButton;


    public UnlockRow(int id, string name, string description, string short_description, int price)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.short_description = short_description;
        this.price = price;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void notEnough()
    {
        buyButton.interactable = false;
    }

    public void enough()
    {
        buyButton.interactable = true;
    }
}
