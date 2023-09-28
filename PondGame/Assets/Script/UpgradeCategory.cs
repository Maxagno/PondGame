using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCategory : MonoBehaviour
{
    public Fish[] list;

    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.
    public List<GameObject> listRow = new List<GameObject>();
    public Transform content; // Référence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

    public GameObject PanelManager;
    private PanelManager panelManager;

    private List<int> listOfCost = new List<int>();

    public int amountBuy = 1;

    private void Start()
    {
        // Crée les articles de la boutique et les ajoute à la ScrollView.
        GenerateShopItems();
        panelManager = PanelManager.GetComponent<PanelManager>();
    }

    void Update()
    {
        canBeBought();
    }

    private void GenerateShopItems()
    {
        // Assurez-vous que le préfab d'article et le contenu sont définis.
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Préfab ou contenu non défini dans le gestionnaire de boutique.");
            return;
        }
        GameObject newItem = Instantiate(itemPrefab, content);
        ShopRow row_tmp = newItem.GetComponent<ShopRow>();
        initClickRow(row_tmp);
        newItem.SetActive(true);
        listRow.Add(newItem);
        listOfCost.Add(1);
        // Génère les articles de la boutique.
        for (int i = 1; i < numberOfItems; i++)
        {
            // Instancie l'article de préfab.
            newItem = Instantiate(itemPrefab, content);
            row_tmp = newItem.GetComponent<ShopRow>();
            initRow(row_tmp, i, i, 1, "Test", "test", i, i);
            listOfCost.Add(i);
            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, définissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez également ajouter un bouton d'achat et définir son comportement ici.

            // Assurez-vous que l'article est correctement configuré.
            newItem.SetActive(true);
            listRow.Add(newItem);
        }
    }

    

    public void onClick_Row(Button button)
    {
        ShopRow clickedRow = button.transform.parent.GetComponent<ShopRow>();
        int cost = clickedRow.getCost();
        int result = panelManager.BuyUpgrade(cost);

        // Can be bought
        if (result == 0)
        {
            Debug.Log(clickedRow.getLevel());

            if (clickedRow.getLevel() == 0)
            {
                panelManager.newFish(clickedRow.getFishId(), clickedRow.getZoneId());
            }
            if (clickedRow.getZoneId() == 0)
            {
                clickedRow = panelManager.upgradeClick(amountBuy, clickedRow);
                listOfCost[clickedRow.getFishId()] = clickedRow.getCost();
                clickedRow.initText();
            } else {
                clickedRow.setProduction(updateProduction(clickedRow.getProduction(), amountBuy));
                cost = updateCost(cost, amountBuy);
                listOfCost[clickedRow.id] = cost;
                clickedRow.setCost(cost);
                clickedRow.setLevel(updateLevel(clickedRow.getLevel(), amountBuy));
                clickedRow.initText();
            }
            //panelManager.updateUpgradePrice();
        }
        //clickedRow.fish
        //Debug.Log(clickedRow.id);
    }

    public void onClick_UpdateAmount(int amount)
    {
        amountBuy = amount;
    }

    // GET SET


    // PRIVATE FUNCTION

    private void canBeBought()
    {
        int money = panelManager.getMoney();
        for (int i = 0; i < listOfCost.Count; i++)
        {
            ShopRow currRow = listRow[i].GetComponent<ShopRow>();
            if (listOfCost[i] > money)
            {
                currRow.notEnough();
            } else
            {
                currRow.enough();
            }
        }
    }

    private void initClickRow(ShopRow row)
    {
        row.id = 0;
        row.fishId = 0;
        row.zoneId = 0;
        row.fishName = "Click";
        row.fishDescription = "Test";
        row.fishproduction = 1;
        row.fishPrice = 1;
        row.fishLevel = 1;
        row.buyButton.onClick.AddListener(delegate { onClick_Row(row.buyButton); });
        row.initText();
    }
    //TODO Create another initFunc
    private void initRow(ShopRow row, int id, int fishId, int zoneId, string name, string description, int production, int price)
    {
        row.id = id;
        row.fishId = fishId;
        row.zoneId = zoneId;
        row.fishName = name;
        row.fishDescription = description;
        row.fishproduction = production;
        row.fishPrice = price;
        row.fishLevel = 0;
        row.buyButton.onClick.AddListener(delegate { onClick_Row(row.buyButton); });
        row.initText();
    }


    private int updateProduction(int production, int amount_lvlUp = 1) 
    {
        int tmp = production;
        production += amount_lvlUp;
        tmp = production - tmp;
        panelManager.updateProduction(tmp);
        return production;
    }

    private int updateCost(int cost, int amount_lvlUp = 1)
    {
        cost += ((cost/20) + 2 + amount_lvlUp) * 2;
        return cost;
    }

    private int updateLevel(int level, int amount_lvlUp = 1)
    {
        level += amount_lvlUp;
        return level;
    }
}
