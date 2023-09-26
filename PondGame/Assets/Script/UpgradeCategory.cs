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

    public GameObject pannelManager;


    private void Start()
    {
        // Crée les articles de la boutique et les ajoute à la ScrollView.
        GenerateShopItems();
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
        // Génère les articles de la boutique.
        for (int i = 1; i < numberOfItems; i++)
        {
            // Instancie l'article de préfab.
            newItem = Instantiate(itemPrefab, content);
            row_tmp = newItem.GetComponent<ShopRow>();
            initRow(row_tmp, i, i, i, "Test", "test", i, i);
            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, définissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez également ajouter un bouton d'achat et définir son comportement ici.

            // Assurez-vous que l'article est correctement configuré.
            newItem.SetActive(true);
            listRow.Add(newItem);
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
        row.fishLevel = 1;
        row.buyButton.onClick.AddListener(delegate { onClick_Row(row.buyButton); });
        row.initText();
    }

    public void onClick_Row(Button button)
    {
        ShopRow clickedRow = button.transform.parent.GetComponent<ShopRow>();
        //clickedRow.fish
        Debug.Log(clickedRow.id);
    }
}
