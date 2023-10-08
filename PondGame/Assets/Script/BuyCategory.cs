using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCategory : MonoBehaviour
{
    public GameObject itemPrefab; // R�f�rence au pr�fab d'article de la boutique.

    public List<GameObject> listRow = new List<GameObject>();

    public Transform content; // R�f�rence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

    public GameObject PanelManager;
    private PanelManager panelManager;
    // Start is called before the first frame update
    void Start()
    {
        // Cr�e les articles de la boutique et les ajoute � la ScrollView.
        GenerateShopItems();
        panelManager = PanelManager.GetComponent<PanelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        List<InitInfo> result = new List<InitInfo>();
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Pr�fab ou contenu non d�fini dans le gestionnaire de boutique.");
            return result;
        }

        // INIT Click ROW
        GameObject newItem = Instantiate(itemPrefab, content);
        newItem.SetActive(true);
        listRow.Add(newItem);

        for (int i = 0; i < infoForPanel.Count; i++)
        {
            InitInfo currInfo = infoForPanel[i];
            /*newItem = Instantiate(itemPrefab, content);
            newItem.SetActive(true);
            listRow.Add(newItem);*/
            List<doubleInt> listOfLink = currInfo.listOfLink;
            for (int j = 0; j < listOfLink.Count; j++)
            {
                InitInfo temp = new InitInfo();
                temp.initInfoRow(currInfo.zoneId, listOfLink[j].item1, listOfLink[j].item2, ((j * i) + 5));
                newItem = Instantiate(itemPrefab, content);
                UnlockRow uRow = newItem.GetComponent<UnlockRow>();
                uRow.price = (j * i) + 5;
                newItem.SetActive(true);
                listRow.Add(newItem);
                result.Add(temp);
            }
        }
        return result;
    }
    
}
