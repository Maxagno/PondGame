using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCategory : MonoBehaviour
{
    public GameObject itemPrefab; // R�f�rence au pr�fab d'article de la boutique.

    public List<GameObject> listRow = new List<GameObject>();

    public Transform content; // R�f�rence au contenu de la ScrollView.
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
        // Cr�e les articles de la boutique et les ajoute � la ScrollView.
        GenerateShopItems();
        panelManager = PanelManager.GetComponent<PanelManager>();
    }

    void Update()
    {
        canBeBought(amountBuy);
    }

    private void GenerateShopItems()
    {
        // Assurez-vous que le pr�fab d'article et le contenu sont d�finis.
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Pr�fab ou contenu non d�fini dans le gestionnaire de boutique.");
            return;
        }

        // INIT Click ROW
        GameObject newItem = Instantiate(itemPrefab, content);
        DataFish Clicker = new DataFish(0, "Click", "", 1, 1, 1);
        ShopRow row_tmp = newItem.GetComponent<ShopRow>();
        initRow(row_tmp, Clicker, 0, 0);
        newItem.SetActive(true);
        listOfFish.Add(Clicker);
        Debug.Log("Level  " + Clicker.level);
        listRow.Add(newItem);
        listOfCurrCost.Add(Clicker.cost);
        listOfCost.Add(Clicker.cost);
        listOfamountLevel.Add(1);
        // G�n�re les articles de la boutique.
        for (int i = 1; i < numberOfItems; i++)
        {
            // Instancie l'article de pr�fab.
            DataFish fish = new DataFish(i, "NameOfFish_" + i, "", i, i, 0);
            newItem = Instantiate(itemPrefab, content);
            row_tmp = newItem.GetComponent<ShopRow>();
            initRow(row_tmp, fish, i, 1);
            listOfFish.Add(fish);
            listOfCurrCost.Add(fish.cost);
            listOfCost.Add(fish.cost);
            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, d�finissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez �galement ajouter un bouton d'achat et d�finir son comportement ici.

            // Assurez-vous que l'article est correctement configur�.
            newItem.SetActive(true);
            listRow.Add(newItem);
            listOfamountLevel.Add(1);
            listOfProd.Add(0);
        }
    }

    

    public void onClick_Row(Button button)
    {
        ShopRow clickedRow = button.transform.parent.GetComponent<ShopRow>();
        int id = clickedRow.getId();
        DataFish fish = listOfFish[id];
        int level = listOfamountLevel[id];
        int result = panelManager.BuyUpgrade(listOfCost[id]);

        // Can be bought
        if (result == 0)
        {
            int fishId = clickedRow.getFishId();

            // Check if it's the click that is being upgraded
            if (id == 0)
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
                fish.cost = listOfCost[id];
                fish.production = updateProduction(fish.production, level);
                listOfProd[id - 1] = fish.production;
                updateProductionStat();
            }
            listOfCurrCost[id] = listOfCost[id];
            clickedRow.initText(fish.name, fish.level, fish.production, fish.cost);
            //panelManager.updateUpgradePrice();
        }
    }

    public void onClick_UpdateAmount(int amount)
    {
        amountBuy = amount;
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
            if (listOfCost[i] > money)
            {
                currRow.notEnough();
            } else
            {
                currRow.enough();
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
