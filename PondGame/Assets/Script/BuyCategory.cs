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
            
            GameObject newItem = Instantiate(itemPrefab, content);
            
            // INIT THE ROW TO UNLOCK THE ZONE
            UnlockRow uRow = newItem.GetComponent<UnlockRow>();
            uRow.buyButton.onClick.AddListener(delegate { onClick_Row(newItem); });
            uRow.initUnlockRow(idForRow, currInfo.zoneName, "description", "short / Unlock The Zone ", idForRow);
            temp.initInfoRow(currInfo.zoneId, idForRow);
            idForRow++;
            newItem.SetActive(true);
            listRow.Add(newItem);

            // ADD THE NEW INFO TO THE RESULT
            resultInit.Add(temp);



            List<InitInfoFish> listInfoFish = currInfo.listInfoFish;


            // CREATING THE ROW TO UNLOCK NEW FISH
            for (int j = 0; j < listInfoFish.Count; j++)
            {
                InitInfoFish infoFishTMP = listInfoFish[j];
                // CHECK IF THE FISH IS BEING BLOCKED

                if (infoFishTMP.blockObject != null)
                {
                    // INIT INFO TO UNLOCK NEW FISH
                    temp.initInfoRow(currInfo.zoneId, idForRow);

                    

                    newItem = Instantiate(itemPrefab, content);
                    uRow = newItem.GetComponent<UnlockRow>();
                    uRow.initUnlockRow(idForRow, currInfo.zoneName, "Unlock a new fish", "short", idForRow);
                    uRow.setBlock(infoFishTMP.blockObject);
                    newItem.SetActive(true);

                    // ADD FISH INFO
                    temp.listInfoFish.Add(infoFishTMP);


                    listRow.Add(newItem);

                    // ADD THE NEW INFO TO THE RESULT
                    resultInit.Add(temp);
                }
            }
        }
        return resultInit;
    }

    public void onClick_Row(GameObject row)
    {
        UnlockRow clickedRow = row.GetComponent<UnlockRow>();
        int id = clickedRow.getId();
        int result = panelManager.BuyUpgrade(clickedRow.getPrice());
        if (result == 0)
        {
            // TODO MAKE THE BLOCK DISAPEAR AND ENABLE THE ROW IN UPGRADE CATEGORY
            row.SetActive(false);
            if (clickedRow.blockObject != null)
            {
                GameObject blockObject = clickedRow.blockObject;
                blockObject.SetActive(false);

            }
        }
    }

}
