using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour, IDataPersistence
{

    public static GameManager instance;

    private bool hasInitialized = false;

    public string lastSceneName = "";

    public void LoadData(GameData data)
    {
        this.lastSceneName = data.lastSceneName;
    }

    public void SaveData(ref GameData data)
    {
        data.lastSceneName = this.lastSceneName;
    }

    public void changingScene(string sceneName)
    {
        Debug.Log(sceneName);
        this.lastSceneName = sceneName;
    }

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
