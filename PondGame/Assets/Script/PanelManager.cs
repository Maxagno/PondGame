using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelManager : MonoBehaviour
{

    public GameObject UpgradePanel;
    public GameObject BuyCategory;
    public GameObject MenuPanel;

    public GameObject Clicker;
    public GameObject GameManager;

    private UpgradeCategory upgradeCategory;
    private BuyCategory buyCategory;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        upgradeCategory = UpgradePanel.GetComponent<UpgradeCategory>();
        buyCategory = BuyCategory.GetComponent<BuyCategory>();
        gameManager = GameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialisePanelManager(List<InitInfo> initInfos)
    {
        List<InitInfo> infoToUpgrade = buyCategory.initialiseBuyCategory(initInfos);
        upgradeCategory.initialiseUpgradeCategory(infoToUpgrade);
        updateUnlocked(0, -1);
    }

    public int BuyUpgrade(AmountMoney cost)
    {
        AmountMoney money = gameManager.getMoney();
        if (compareIsInfAmount(cost, money))
        {
            gameManager.updateBoughtMoney((cost));
            return 0;
        }
        return 1;
    }

    public (ShopRow, AmountMoney) upgradeClick(int amount, ShopRow clickRow)
    {
        return gameManager.upgradeClick(amount, clickRow);
    }

    public void updateProduction(AmountMoney prod)
    {
        gameManager.updateProduction(prod);
    }

    public void newFish(int id, int zoneId)
    {
        gameManager.newFish(id, zoneId);
    }

    public void updateUnlocked(int zoneId, int fishId)
    {
        upgradeCategory.updateUnlocked(zoneId, fishId);
    }

    //GET SET


    public AmountMoney getMoney()
    {
        return gameManager.getMoney();
    }

    // GESTION OF THE PANEL


    // Called when enabling the menu
    public void ToggleMenu()
    {
        if (MenuPanel.activeSelf)
        {
            MenuPanel.SetActive(false);
        }
        else
        {
            MenuPanel.SetActive(true);
        }
    }


    // TODO : Set all panel to false as to only have one enable // TODO : Rationalize all of the code to make it shorter and more beautiful
    public void onClickCategory(Button button)
    {
        // Check which button has been clicked 
        if (button.name == "UpgradeCategoryButton")
        {
            if (UpgradePanel.activeSelf)
            {
                // If the menu pannel is off then open it, if not the close it
                if (MenuPanel.activeSelf)
                {
                    MenuPanel.SetActive(false);
                }
                else
                {
                    MenuPanel.SetActive(true);
                }
            }
            else
            {
                MenuPanel.SetActive(true);
                UpgradePanel.SetActive(true);
                BuyCategory.SetActive(false);
            }
        }
        if (button.name == "BuyCategoryButton")
        {
            if (BuyCategory.activeSelf)
            {
                if (MenuPanel.activeSelf)
                {
                    MenuPanel.SetActive(false);
                }
                else
                {
                    MenuPanel.SetActive(true);
                }
            }
            else
            {
                MenuPanel.SetActive(true);
                BuyCategory.SetActive(true);
                UpgradePanel.SetActive(false);
            }
        }
    }

    // Private Method
    private bool compareIsInfAmount(AmountMoney amountOne, AmountMoney amountTwo)
    {
        int indexOne = amountOne.getIndexForLetter(amountOne.letter);
        int indexTwo = amountTwo.getIndexForLetter(amountTwo.letter);
        if (indexOne < indexTwo)
        {
            return true;
        }
        else if (indexOne > indexTwo)
        {
            return false;
        }
        return amountOne.listGold[indexOne] <= amountTwo.listGold[indexTwo];
    }

}
