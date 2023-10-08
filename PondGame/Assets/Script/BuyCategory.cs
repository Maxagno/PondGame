using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCategory : MonoBehaviour
{
    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.

    public List<GameObject> listRow = new List<GameObject>();

    public Transform content; // Référence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

    public GameObject PanelManager;
    private PanelManager panelManager;
    // Start is called before the first frame update
    void Start()
    {
        // Crée les articles de la boutique et les ajoute à la ScrollView.
        //GenerateShopItems();
        panelManager = PanelManager.GetComponent<PanelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        newItem.SetActive(true);
        listRow.Add(newItem);

        for (int i = 1; i < numberOfItems; i++)
        {
            newItem = Instantiate(itemPrefab, content);

            newItem.SetActive(true);
            listRow.Add(newItem);
        }
    }

    public List<InitInfo> initialiseBuyCategory(List<InitInfo> infoForPanel)
    {
        Debug.Log("Number of initInfo : " + infoForPanel.Count);
        List<InitInfo> result = new List<InitInfo>();
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Préfab ou contenu non défini dans le gestionnaire de boutique.");
            return result;
        }

        int idForRow = 0;
        for (int i = 0; i < infoForPanel.Count; i++)
        {
            InitInfo temp = new InitInfo();
            InitInfo currInfo = infoForPanel[i];
            GameObject newItem = Instantiate(itemPrefab, content);
            UnlockRow uRow = newItem.GetComponent<UnlockRow>();
            uRow.buyButton.onClick.AddListener(delegate { onClick_Row(newItem); });
            uRow.initUnlockRow(idForRow, currInfo.zoneName, "description", "short / Unlock The Zone ", idForRow);
            idForRow++;
            temp.initInfoRow(currInfo.zoneId, currInfo.fishId, idForRow, idForRow);
            result.Add(temp);
            newItem.SetActive(true);
            listRow.Add(newItem);
            List<doubleInt> listOfLink = currInfo.listOfLink;
            Debug.Log("Currinfo list of link count : " + currInfo.listOfLink.Count);
            Debug.Log("Here is the number for the zone " + i + " : " + listOfLink.Count);
            for (int j = 0; j < listOfLink.Count; j++)
            {
                temp = new InitInfo();
                temp.initInfoRow(currInfo.zoneId, listOfLink[j].item1, listOfLink[j].item2, ((j * i) + 5));
                newItem = Instantiate(itemPrefab, content);
                uRow = newItem.GetComponent<UnlockRow>();
                uRow.initUnlockRow(idForRow, currInfo.zoneName, "description", "short", idForRow);
                newItem.SetActive(true);
                listRow.Add(newItem);
                result.Add(temp);
            }
        }
        return result;
    }

    public void onClick_Row(GameObject row)
    {
        UnlockRow clickedRow = row.GetComponent<UnlockRow>();
        int id = clickedRow.getId();
        int result = panelManager.BuyUpgrade(clickedRow.getPrice());
        if (result == 0)
        {
            row.SetActive(false);
        }
    }

}
