using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Fish[] list;

    public GameObject itemPrefab; // Référence au préfab d'article de la boutique.
    public Transform content; // Référence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

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

        // Génère les articles de la boutique.
        for (int i = 0; i < numberOfItems; i++)
        {
            // Instancie l'article de préfab.
            GameObject newItem = Instantiate(itemPrefab, content);

            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, définissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez également ajouter un bouton d'achat et définir son comportement ici.

            // Assurez-vous que l'article est correctement configuré.
            newItem.SetActive(true);
        }
    }
}
