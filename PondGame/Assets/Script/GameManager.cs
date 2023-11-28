using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private bool hasInitialized = false;



    void Update()
    {
    }

    private void Awake()
    {
        if (!hasInitialized) // Only for the editor mode
        {
            // Effectuez vos opérations d'initialisation ici.
            hasInitialized = true;
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Garantit que l'objet GameManager ne sera pas détruit entre les scènes.
            }
            else
            {
                Debug.Log("BIG Problem ");
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        Awake();
    }
    


}
