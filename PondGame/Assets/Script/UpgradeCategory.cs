using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCategory : MonoBehaviour
{
    public Fish[] list;

    public GameObject itemPrefab; // R�f�rence au pr�fab d'article de la boutique.
    public List<GameObject> listRow = new List<GameObject>();
    public Transform content; // R�f�rence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

    public GameObject pannelManager;


    private void Start()
    {
        // Cr�e les articles de la boutique et les ajoute � la ScrollView.
        GenerateShopItems();
    }

    private void GenerateShopItems()
    {
        // Assurez-vous que le pr�fab d'article et le contenu sont d�finis.
        if (itemPrefab == null || content == null)
        {
            Debug.LogError("Pr�fab ou contenu non d�fini dans le gestionnaire de boutique.");
            return;
        }
        GameObject newItem = Instantiate(itemPrefab, content);
        ShopRow row_tmp = newItem.GetComponent<ShopRow>();
        initClickRow(row_tmp);
        newItem.SetActive(true);
        listRow.Add(newItem);
        // G�n�re les articles de la boutique.
        for (int i = 1; i < numberOfItems; i++)
        {
            // Instancie l'article de pr�fab.
            newItem = Instantiate(itemPrefab, content);
            row_tmp = newItem.GetComponent<ShopRow>();
            initRow(row_tmp, i, i, i, "Test", "test", i, i);
            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, d�finissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez �galement ajouter un bouton d'achat et d�finir son comportement ici.

            // Assurez-vous que l'article est correctement configur�.
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
