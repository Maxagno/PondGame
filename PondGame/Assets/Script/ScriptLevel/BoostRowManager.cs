using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostRowManager : MonoBehaviour
{
    private LevelManager levelManager;

    public BoostRowManager instanceSelf;
    public List<GameObject> listRow_GameObject = new List<GameObject>();
    public List<BoostLevel> listBoostRow = new List<BoostLevel>();

    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.

    public Transform content; // Référence au contenu de la ScrollView.

    public void setInfo(LevelManager levelManager, List<BoostLevel> listBoostRow)
    {
        instanceSelf = this;
        if (levelManager == null)
        {
            Debug.LogError("No levelManager instantiated or linked");
        }
        foreach (BoostLevel boost in listBoostRow)
        {
            if (boost != null)
            {
                Debug.Log(boost.name);

                GameObject newItem = Instantiate(itemPrefab, content);
                newItem.SetActive(true);
                BoostLevel boostRow = newItem.GetComponent<BoostLevel>();
                boostRow.copy(boost);
                this.listBoostRow.Add(boostRow);
                listRow_GameObject.Add(newItem);
            }
        }
    }
    public int updateBoughtMoney(AmountMoney amount = null)
    {
        return levelManager.updateBoughtMoney(amount);
    }

    public void UpdateCanBeBought()
    {
        AmountMoney money = levelManager.getMoney();
        for (int i = 0; i < listBoostRow.Count; i++)
        {
            if (listBoostRow[i] != null)
            {
                AmountMoney tmp_Cost = listBoostRow[i].cost;
                if (tmp_Cost.amount.CompareTo(money.getAmount()) > 0)
                {
                    listBoostRow[i].buyButton.interactable = false;
                }
                else
                {
                    listBoostRow[i].buyButton.interactable = true;
                }
            }
        }
    }

    public void hideRow(int id)
    {
        listRow_GameObject[id].SetActive(false);
    }
}
