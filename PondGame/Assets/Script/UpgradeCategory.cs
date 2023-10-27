using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCategory : MonoBehaviour
{
    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.

    public List<GameObject> listRow = new List<GameObject>();
    public List<GameObject> listButtonAmount;

    public Transform content; // Référence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

    public GameObject PanelManager;
    private PanelManager panelManager;

    public List<Unlocked> listUnlocked = new List<Unlocked>();

    private List<AmountMoney> listOfCost = new List<AmountMoney>();
    private List<AmountMoney> listOfCurrCost = new List<AmountMoney>();

    private List<int> listOfamountLevel = new List<int>();

    private List<DataFish> listOfFish = new List<DataFish>();

    private List<AmountMoney> listOfProd = new List<AmountMoney> ();

    public int amountBuy = 1;

    private void Start()
    {
        panelManager = PanelManager.GetComponent<PanelManager>();
    }

    void Update()
    {
        canBeBought(amountBuy);
    }
        
    public void initialiseUpgradeCategory(List<InitInfo> initInfos)
    {
        // Assurez-vous que le préfab d'article et le contenu sont définis.
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Préfab ou contenu non défini dans le gestionnaire de boutique.");
            return;
        }

        // INIT Click ROW
        GameObject newItem = Instantiate(itemPrefab, content);
        DataFish Clicker = new DataFish(-1, "Click", "", new AmountMoney(1,""), new AmountMoney(1,""), 1);
        ShopRow row_tmp = newItem.GetComponent<ShopRow>();
        initRow(row_tmp, Clicker, -1, -1);
        row_tmp.isLocked = false;
        newItem.SetActive(true);
        
        // ADDING THE ROW TO ALL LIST
        listOfFish.Add(Clicker);
        listRow.Add(newItem);
        listOfCurrCost.Add(Clicker.cost);
        AmountMoney tmpCost = new AmountMoney(0, "");
        tmpCost.updateAllAmount(Clicker.cost);
        listOfCost.Add(tmpCost);
        listOfamountLevel.Add(1);

        int rowId = 0;
        // Adding the rest of the possible fish to buy
        for (int i = 0; i < initInfos.Count; i++)
        {
            // Instantiate the prefab
            InitInfo currRow = initInfos[i];


            List<InitInfoFish> listInfoFish = currRow.listInfoFish;

            // CREATING THE ROW TO UNLOCK NEW FISH
            for (int j = 0; j < listInfoFish.Count; j++)
            {
                InitInfoFish infoFishTMP = listInfoFish[j];
                // GET THE INFO FOR THE ROW

                DataFish fish = new DataFish(rowId, infoFishTMP.fishName, infoFishTMP.fishDescription, new AmountMoney(i+j, ""), new AmountMoney(i+j, ""));

                rowId++;

                newItem = Instantiate(itemPrefab, content);
                row_tmp = newItem.GetComponent<ShopRow>();
                initRow(row_tmp, fish, infoFishTMP.fishId, currRow.zoneId);
                newItem.SetActive(false);

                listOfFish.Add(fish);
                listOfCurrCost.Add(fish.cost);
                tmpCost = new AmountMoney(0, "");
                tmpCost.updateAllAmount(fish.cost);
                listOfCost.Add(tmpCost);
                listRow.Add(newItem);
                listOfamountLevel.Add(1);
                listOfProd.Add(new AmountMoney(0,""));
            }
        }
    }

    public void updateUnlocked(int zoneId, int fishId)
    {
        for (int i = 0; i < listUnlocked.Count; i++)
        {
            Unlocked currZone = listUnlocked[i];
            if (currZone.zoneId == zoneId)
            {
                currZone.listFishId.Add(fishId);
                updateHiddenRows();
                return;
            }
            else if(currZone.zoneId > zoneId)
            {
                break;
            }
        }
        Unlocked newZone = new Unlocked();
        newZone.zoneId = zoneId;
        if (fishId != -1)
        {
            newZone.listFishId.Add(fishId);
        }
        listUnlocked.Add(newZone);
        updateHiddenRows();
        return;
    }

    
    private void updateHiddenRows()
    {
        int indexUnlocked = 0;
        for (int i = 0; i < listRow.Count; i++)
        {
            if (indexUnlocked >= listUnlocked.Count)
            {
                return;
            }
            ShopRow currRow = listRow[i].GetComponent<ShopRow>();
            Unlocked currZone = listUnlocked[indexUnlocked];
            if(currRow.zoneId > currZone.zoneId) 
            { 
                i--;
                indexUnlocked++;
            } else
            {
                listRow[i].SetActive(true);
                if (currRow.isLocked)
                {
                    for (int j = 0; j < currZone.listFishId.Count; j++)
                    {
                        if (currRow.fishId == currZone.listFishId[j])
                        {
                            currRow.isLocked = false;
                            break;
                        }
                    }
                }
            }
        }
    }



    public void onClick_Row(Button button)
    {
        ShopRow clickedRow = button.transform.parent.GetComponent<ShopRow>();
        int id = clickedRow.getId();
        DataFish fish = listOfFish[id + 1];
        int level = listOfamountLevel[id+1];
        int result = panelManager.BuyUpgrade(listOfCost[id+1]);

        Debug.Log("The result is : " + result + "  the Id of the row is : " + id + " and the current level of the fish is : " + fish.level);
        Debug.Log("Before levelUp, the fish is lvl: " + fish.level + "| Prod: " + fish.production.ToString() + "| Price: " + fish.cost.ToString()); 
        // Can be bought
        if (result == 0)
        {
            int fishId = clickedRow.getFishId();

            // Check if it's the click that is being upgraded
            if (id == -1)
            {
                (clickedRow, fish.cost) = panelManager.upgradeClick(level, clickedRow);
                fish.level += level;
                listOfCurrCost[id + 1] = fish.cost;
                fish.production.updateAmount(fish.level, fish.production.letter);
            }
            else 
            { // Is it a new fish that need to be spawned 
                if (fish.level == 0)
                {
                    panelManager.newFish(fishId, clickedRow.getZoneId());
                    fish.level = level;
                    listOfProd[id] = fish.production;
                }
                else
                {
                    fish.level += level;
                }
                fish.cost = updateCost(fish.cost, amountBuy);
                listOfCurrCost[id + 1] = fish.cost;
                fish.production = updateProduction(fish.production, level);

                updateProductionStat();
            }
            Debug.Log("Cost of fish : " + fish.cost.ToString());
            Debug.Log("Cost of fish in ListCurrCost : " + listOfCurrCost[id+1].ToString());

            clickedRow.initText(fish.name, fish.level, fish.production, fish.cost);
        }
    }

    public void onClick_UpdateAmount(Button button)
    {
        button.interactable = false;
        int i = 0;
        int j = 1;
        if (button.name == "One") {
            amountBuy = 1;
            i = 2;
        } else if (button.name == "Two")
        {
            j = 2;
            amountBuy = 10;
        } else
        {
            amountBuy = 25;
        }
        listButtonAmount[i].GetComponent<Button>().interactable = true;
        listButtonAmount[j].GetComponent<Button>().interactable = true;
    }

    // PRIVATE FUNCTION

    private void canBeBought(int amount = 1)
    {
        //Debug.Log(" Amount put :  " + amount);
        AmountMoney money = panelManager.getMoney();
        updateListOfCost(amount, money);
        for (int i = 0; i < listOfCost.Count; i++)
        {
            //Debug.Log("Cost of " + i + " : " + listOfCost[i]);
            ShopRow currRow = listRow[i].GetComponent<ShopRow>();
            currRow.UpdateCost(listOfCost[i]);
            if (!compareIsInfAmount(listOfCost[i], money) || currRow.getIsLocked())
            {
                currRow.canNotBeBought();
            } else {
                currRow.canBeBought();
            }
        }
    }

    private void updateListOfCost(int amount, AmountMoney money)
    {
        for (int i = 0; i < listOfCurrCost.Count; i++)
        {
            listOfCost[i].copyAmount(listOfCurrCost[i]);
            listOfamountLevel[i] = 1;
        }
        //listOfCost = listOfCurrCost;
        if (amount > 1)
        {
            for (int i = 0;i < listOfCost.Count;i++)
            {
                ShopRow row = listRow[i].GetComponent<ShopRow>();
                if (!row.isLocked)
                {
                    untillMaxAmount(money, i, amount);
                }
            }
        }
    }

    private void untillMaxAmount(AmountMoney money, int index, int amountLvlMax)
    {
        if (compareIsInfAmount(listOfCost[index], money))
        {
            AmountMoney tempCost = listOfCost[index];
            int temp = 1;
            while (compareIsInfAmount(tempCost,money) && temp <= amountLvlMax) 
            {
                tempCost = updateCost(tempCost, 1);
                if (compareIsInfAmount(tempCost,money))
                {
                    listOfCost[index] = (tempCost);
                    temp++;
                }
            }
            if (temp <= amountLvlMax)
            {
                listOfamountLevel[index] = temp;
            } else
            {
                listOfamountLevel[index] = amountLvlMax;
            }
        }
    }

    //TODO Create another initFunc
    private void initRow(ShopRow row, DataFish fish, int fishId, int zoneId)
    {
        row.id = fish.id;
        row.fishId = fishId;
        row.zoneId = zoneId;
        row.fishName = fish.name;
        row.fishDescription = fish.description;
        row.buyButton.onClick.AddListener(delegate { onClick_Row(row.buyButton); });
        row.initText(fish.name, fish.level, fish.production, fish.cost);
        row.isLocked = true;
    }


    private AmountMoney updateProduction(AmountMoney production, int amount_lvlUp = 1) 
    {
        for (int i = 0; i < amount_lvlUp; i++)
        {
            production.updateAmount(1, "");
        }
        return production;
    }

    private AmountMoney updateCost(AmountMoney cost, int amount_lvlUp = 1)
    {
        AmountMoney result = new AmountMoney(0, "");
        result.copyAmount(cost);
        for (int i = 0; i < amount_lvlUp; i++)
        {
            result.updateAmount(1, "");
        }
        return result;
    }

    private void updateProductionStat()
    {
        AmountMoney amount = new AmountMoney(0, "");

        for (int i = 0; i < listOfProd.Count; i++)
        {
            amount.updateAllAmount(listOfProd[i]);
        }
        panelManager.updateProduction(amount);
    }

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
