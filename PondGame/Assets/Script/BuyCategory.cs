using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCategory : MonoBehaviour
{
    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.

    public List<GameObject> listRow = new List<GameObject>();

    private List<int> listOfCost = new List<int>();

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
        List<InitInfo> resultInit = new List<InitInfo>();
        
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Préfab ou contenu non défini dans le gestionnaire de boutique.");
            return resultInit;
        }

        int idForRow = 0;
        for (int i = 0; i < infoForPanel.Count; i++)
        {
            InitInfo temp = new InitInfo();
            InitInfo currInfo = infoForPanel[i];

            GameObject newItem;
            UnlockRow uRow;
            temp.initInfoRow(currInfo.zoneId, idForRow);
            // INIT THE ROW TO UNLOCK THE ZONE
            if (i != 0)
            {
                newItem = Instantiate(itemPrefab, content);
                uRow = newItem.GetComponent<UnlockRow>();
                uRow.zone = currInfo.zone;
                initRow(uRow, idForRow, currInfo.zoneId, currInfo.zoneName, "Unlock a new zone", "Short / Unlock a zone" + idForRow, idForRow, 0);

                temp.initInfoRow(currInfo.zoneId, idForRow);

                idForRow++;

                newItem.SetActive(true);
                listRow.Add(newItem);
            }

            // ADD THE NEW INFO TO THE RESULT
            //resultInit.Add(temp);

            List<InitInfoFish> listInfoFish = currInfo.listInfoFish;

            // CREATING THE ROW TO UNLOCK NEW FISH
            for (int j = 0; j < listInfoFish.Count; j++)
            {
                InitInfoFish infoFishTMP = listInfoFish[j];
                // CHECK IF THE FISH IS BEING BLOCKED

                if (infoFishTMP.blockObject != null)
                {
                    // INIT INFO TO UNLOCK NEW FISH
                    newItem = Instantiate(itemPrefab, content);
                    uRow = newItem.GetComponent<UnlockRow>();
                    initRow(uRow, idForRow, currInfo.zoneId, currInfo.zoneName, "Unlock a new fish", "Short" + idForRow, idForRow, 1, infoFishTMP);

                    newItem.SetActive(true);
                    idForRow++;
                    // ADD FISH INFO
                    temp.listInfoFish.Add(infoFishTMP);


                    listRow.Add(newItem);

                    // ADD THE NEW INFO TO THE RESULT
                    
                } else {
                    temp.listInfoFish.Add(infoFishTMP);
                }
            }
            resultInit.Add(temp);
        }
        isHidden();
        return resultInit;
    }

    public void onClick_Row(Button button)
    {
        GameObject row = button.transform.parent.gameObject;
        UnlockRow clickedRow = row.GetComponent<UnlockRow>();

        int result = panelManager.BuyUpgrade(clickedRow.getPrice());

        if (result == 0)
        {
            panelManager.updateUnlocked(clickedRow.zoneId, clickedRow.fishId);
            // ENABLE THE ROW IN UPGRADE CATEGORY
            //Row is unlocking a zone
            if (clickedRow.categoryRow == 0)
            {
                clickedRow.zone.SetActive(true);
            }

            // REMOVING THE ROW AND THE BLOCK IF THERE IS ONE
            row.SetActive(false);
            if (clickedRow.blockObject != null)
            {
                GameObject blockObject = clickedRow.blockObject;
                blockObject.SetActive(false);

            }
            listRow.Remove(row);
        }
        isHidden();
    }

    /*
     * Private Function
    */


    private void canBeBought()
    {
        //Debug.Log(" Amount put :  " + amount);
        int money = panelManager.getMoney();

        for (int i = 0; i < listRow.Count; i++)
        {
            //Debug.Log("Cost of " + i + " : " + listOfCost[i]);
            UnlockRow currRow = listRow[i].GetComponent<UnlockRow>();
            if (currRow.price <= money && listRow[i].activeSelf )
            {
                currRow.canBeBought();
            }
            else
            {
                currRow.canNotBeBought();
            }
        }
    }

    // INIT THE ROW : NECESSARY FOR THE DELEGATE FUNCTION
    private void initRow(UnlockRow row, int id, int zoneId, string zoneName, string description, string shortDesc, int cost, int category, InitInfoFish infoFishTMP = null)
    {
        row.initUnlockRow(id, zoneId, zoneName, description, shortDesc, cost, category);
        if (category == 1)
        {
            row.setBlock(infoFishTMP.blockObject);
            row.setfishId(infoFishTMP.fishId);
        }
        row.buyButton.onClick.AddListener(delegate { onClick_Row(row.buyButton); });
    }

    private void isHidden()
    {
        int currZone = 100;
        for (int i = 0; i < listRow.Count; i++)
        {
            UnlockRow currRow = listRow[i].GetComponent<UnlockRow>();
            //TODO hide zone that is still blocked by another zone
            if (currRow.categoryRow == 0)
            {
                if (currRow.zoneId <= currZone)
                {
                    currZone = currRow.zoneId;
                    listRow[i].gameObject.SetActive(true);
                } else if (currRow.zoneId == currZone + 1) {
                    listRow[i].gameObject.SetActive(true);
                } else
                {
                    listRow[i].gameObject.SetActive(false);
                }
            } else if (currRow.categoryRow == 1)
            {

                if(currRow.zoneId <= currZone)
                {
                    currZone = currRow.zoneId;
                    listRow[i].gameObject.SetActive(true);
                } else
                {
                    listRow[i].gameObject.SetActive(false);
                }
            }
        }
    }

}
