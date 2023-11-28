using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRowManager : MonoBehaviour
{
    private LevelManager levelManager;

    public FishRowManager instanceSelf;
    public List<GameObject> listRow_GameObject = new List<GameObject>();
    public List<FishRow> listFishRow = new List<FishRow>();
    public ClickRow clickRow;

    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.
    public GameObject clickRowPrefab; // Référence au préfab d'article de la boutique.

    public Transform content; // Référence au contenu de la ScrollView.
    
    public void setInfo(LevelManager levelManager,ClickerLevel clicker, List<FishLevel> listFish)
    {
        instanceSelf = this;
        if (levelManager == null)
        {
            Debug.LogError("No levelManager instantiated or linked");
        }
        if (clicker != null)
        {
            Debug.Log(clicker.getLevel());
            this.levelManager = levelManager;
            GameObject newItem = Instantiate(clickRowPrefab, content);
            newItem.SetActive(true);
            ClickRow clickRow = newItem.GetComponent<ClickRow>();
            clickRow.setInfo(clicker, instanceSelf);
            this.clickRow = clickRow;
            listRow_GameObject.Add(newItem);
        }
        foreach (FishLevel fish in listFish)
        {
            if (fish != null)
            {
                Debug.Log(fish.getLevel());

                GameObject newItem = Instantiate(itemPrefab, content);
                newItem.SetActive(true);
                FishRow fishRow = newItem.GetComponent<FishRow>();
                fishRow.setInfo(fish, instanceSelf);
                listFishRow.Add(fishRow);
                listRow_GameObject.Add(newItem);
            }
        }
    }
    public int updateBoughtMoney(AmountMoney amount = null)
    {
        return levelManager.updateBoughtMoney(amount);
    }

    public void updateProduction(double amount)
    {
        levelManager.updateProduction(amount);
    }

    public void boostUpgrade()
    {
        foreach (FishRow row in listFishRow)
        {
            row.updateInfo();
        }
    }

    public void updateAmount(int amount)
    {
        clickRow.setAmount(amount);
        for (int i = 0; i < listFishRow.Count; i++)
        {
            listFishRow[i].setAmount(amount);
        }
        UpdateCanBeBought();
    }

    public void UpdateCanBeBought()
    {
        AmountMoney money = levelManager.getMoney();
        if (clickRow != null)
        {
            if (clickRow.cost.amount.CompareTo(money.getAmount()) > 0)
            {
                clickRow.buyButton.interactable = false;
            }
            else
            {
                clickRow.buyButton.interactable = true;
            }
        }
        for (int i = 0; i < listFishRow.Count; i++)
        {
            if (listFishRow[i] != null)
            {
                AmountMoney tmp_Cost = listFishRow[i].cost;
                if (tmp_Cost.amount.CompareTo(money.getAmount()) > 0)
                {
                    listFishRow[i].buyButton.interactable = false;
                }
                else
                {
                    listFishRow[i].buyButton.interactable = true;
                }
            }
        }
    }

}
