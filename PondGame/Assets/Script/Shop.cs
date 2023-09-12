using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Fish[] list;

    public GameObject itemPrefab; // R�f�rence au pr�fab d'article de la boutique.
    public Transform content; // R�f�rence au contenu de la ScrollView.
    public int numberOfItems = 10; // Nombre d'articles dans la boutique.

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

        // G�n�re les articles de la boutique.
        for (int i = 0; i < numberOfItems; i++)
        {
            // Instancie l'article de pr�fab.
            GameObject newItem = Instantiate(itemPrefab, content);

            // Personnalisez l'article ici en fonction de vos besoins.
            // Par exemple, d�finissez l'image, le nom, la description et le prix de l'article.

            // Vous pouvez �galement ajouter un bouton d'achat et d�finir son comportement ici.

            // Assurez-vous que l'article est correctement configur�.
            newItem.SetActive(true);
        }
    }
}
