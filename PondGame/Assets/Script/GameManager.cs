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
            // Effectuez vos op�rations d'initialisation ici.
            hasInitialized = true;
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Garantit que l'objet GameManager ne sera pas d�truit entre les sc�nes.
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
