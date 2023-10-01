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
        GenerateShopItems();
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
    
}
