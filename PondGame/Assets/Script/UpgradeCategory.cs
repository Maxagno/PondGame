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

    private List<int> listOfCost = new List<int>();
    private List<int> listOfCurrCost = new List<int>();

    private List<int> listOfamountLevel = new List<int>();

    private List<DataFish> listOfFish = new List<DataFish>();

    private List<int> listOfProd = new List<int> ();

    public int amountBuy = 1;

    private void Start()
    {
        // Crée les articles de la boutique et les ajoute à la ScrollView.
        //GenerateShopItems();
        panelManager = PanelManager.GetComponent<PanelManager>();
    }

    void Update()
    {
        canBeBought(amountBuy);
    }

    private void GenerateShopItems()
    {
        // Assurez-vous que le préfab d'article et le contenu sont définis.
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Préfab ou contenu non défini dans le gestionnaire de boutique.");
            return;
        }

        // INIT Click ROW
        GameObject newItem = Instantiate(itemPrefab, content);
        DataFish Clicker = new DataFish(-1, "Click", "", 1, 1, 1);
        ShopRow row_tmp = newItem.GetComponent<ShopRow>();
        initRow(row_tmp, Clicker, -1, -1);
        newItem.SetActive(true);
        listOfFish.Add(Clicker);
        Debug.Log("Level  " + Clicker.level);
        listRow.Add(newItem);
        listOfCurrCost.Add(Clicker.cost);
        listOfCost.Add(Clicker.cost);
        listOfamountLevel.Add(1);
        // Génère les articles de la boutique.
        for (int i = 0; i < numberOfItems; i++)
        {
            // Instancie l'article de préfab.
            DataFish fish = new DataFish(i, "NameOfFish_" + i, "", i, i, 0);
            newItem = Instantiate(itemPrefab, content);
            row_tmp = newItem.GetComponent<ShopRow>();
            initRow(row_tmp, fish, i, 1);
            listOfFish.Add(fish);
            listOfCurrCost.Add(fish.cost);
            listOfCost.Add(fish.cost);
            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, définissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez également ajouter un bouton d'achat et définir son comportement ici.

            // Assurez-vous que l'article est correctement configuré.
            newItem.SetActive(true);
            listRow.Add(newItem);
            listOfamountLevel.Add(1);
            listOfProd.Add(0);
        }
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
        DataFish Clicker = new DataFish(-1, "Click", "", 1, 1, 1);
        ShopRow row_tmp = newItem.GetComponent<ShopRow>();
        initRow(row_tmp, Clicker, -1, -1);
        newItem.SetActive(true);
        
        // ADDING THE ROW TO ALL LIST
        listOfFish.Add(Clicker);
        listRow.Add(newItem);
        listOfCurrCost.Add(Clicker.cost);
        listOfCost.Add(Clicker.cost);
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

                DataFish fish = new DataFish(rowId, infoFishTMP.fishName, infoFishTMP.fishDescription, i+j, i+j);

                rowId++;

                newItem = Instantiate(itemPrefab, content);
                row_tmp = newItem.GetComponent<ShopRow>();
                initRow(row_tmp, fish, infoFishTMP.fishId, currRow.zoneId);
                newItem.SetActive(true);

                listOfFish.Add(fish);
                listOfCurrCost.Add(fish.cost);
                listOfCost.Add(fish.cost);
                listRow.Add(newItem);
                listOfamountLevel.Add(1);
                listOfProd.Add(0);
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

        // Can be bought
        if (result == 0)
        {
            int fishId = clickedRow.getFishId();

            // Check if it's the click that is being upgraded
            if (id == -1)
            {
                (clickedRow, fish.cost) = panelManager.upgradeClick(level, clickedRow);
                fish.level += level;
                fish.production = fish.level * 2;
            }
            else 
            { // Is it a new fish that need to be spawned 
                if (fish.level == 0)
                {
                    panelManager.newFish(fishId, clickedRow.getZoneId());
                    fish.level = level;
                }
                else
                {
                    fish.level += level;
                }
                fish.cost = updateCost(listOfCost[id+1]);
                fish.production = updateProduction(fish.production, level);
                listOfProd[id] = fish.production;
                updateProductionStat();
            }
            listOfCurrCost[id+1] = fish.cost;
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
            amountBuy = -1;
        }
        listButtonAmount[i].GetComponent<Button>().interactable = true;
        listButtonAmount[j].GetComponent<Button>().interactable = true;
    }

    // PRIVATE FUNCTION

    private void canBeBought(int amount = 1)
    {
        //Debug.Log(" Amount put :  " + amount);
        int money = panelManager.getMoney();
        updateListOfCost(amount, money);
        for (int i = 0; i < listOfCost.Count; i++)
        {
            //Debug.Log("Cost of " + i + " : " + listOfCost[i]);
            ShopRow currRow = listRow[i].GetComponent<ShopRow>();
            currRow.UpdateCost(listOfCost[i]);
            if (listOfCost[i] > money || currRow.getIsLocked())
            {
                currRow.canNotBeBought();
            } else
            {
                currRow.canBeBought();
            }
        }
    }

    private void updateListOfCost(int amount, int money)
    {
        for (int i = 0; i < listOfCurrCost.Count; i++)
        {
            listOfCost[i] = listOfCurrCost[i];
            listOfamountLevel[i] = 1;
        }
        //listOfCost = listOfCurrCost;
        if (amount == 10)
        {
            for (int i = 0;i < listOfCost.Count;i++)
            {
                listOfCost[i] = updateCost(listOfCost[i], amount);
                listOfamountLevel[i] = 10;
            }
        } else if(amount < 0) {
            toMax(money);
        }
    }

    private void toMax(int money)
    {
        for (int i = 0; i < listOfCost.Count; i++)
        {
            int amountOfLevel = 1;
            int tmp = listOfCost[i];
            while (tmp <= money)
            {
                tmp = updateCost(tmp, 1);
                if (tmp <= money)
                {
                    listOfCost[i] = tmp;
                    amountOfLevel++;
                }
            }
            listOfamountLevel[i] = amountOfLevel;
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
    }


    private int updateProduction(int production, int amount_lvlUp = 1) 
    {
        for (int i = 0; i < amount_lvlUp; i++)
        {
            production = production + 1;
        }
        return production;
    }

    private int updateCost(int cost, int amount_lvlUp = 1)
    {
        for (int i = 0; i < amount_lvlUp; i++)
        {
            cost += 1;
        }
        return cost;
    }

    private void updateProductionStat()
    {
        int amount = 0;
        for(int i = 0; i < listOfProd.Count; i++)
        {
            amount += listOfProd[i];
        }
        panelManager.updateProduction(amount);
    }
}
