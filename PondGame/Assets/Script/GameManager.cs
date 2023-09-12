using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public TMP_Text clickCountText; // Reference to the UI text object that displays the click count
    public TMP_Text clickLevelText;
    public TMP_Text clickUpgradeCostText;
    public TMP_Text clickRevenueText;
    public GameObject menuPanel;
    public Clicker clickerManager;


    private bool hasInitialized = false;


    public GameObject UpgradePanel;
    public GameObject BuyPanel;

    // Variable for the money
    private int money = 0;
    //private Shop shop = new Shop();

    // Variable for the click meca
    private int clickUpgradeCost = 1;

    //private float timeSinceLastResourceGeneration = 0f;
    //private int timeCalled = 0;
    /*private void Update()
    {
        
        // Calculate the current resource generation rate per second based on the current upgrade level
        //float resourceGenerationRate = GetResourceGenerationRate(upgradeLevel);

        // Add the generated resources to the resources field every second
        timeSinceLastResourceGeneration += Time.deltaTime;
        if (timeSinceLastResourceGeneration >= 1f)
        {
            timeCalled++;
            Debug.Log("Call ressourceGeneration " + timeCalled);
            timeSinceLastResourceGeneration= 0f;
            //TODO 
        }
    }*/

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


    public void UpgradeClickLevel()
    {
        if (clickUpgradeCost <= money)
        {
            int tmp = clickerManager.UpgradeClickLevel(1);
            money -= clickUpgradeCost;
            clickUpgradeCost = tmp + clickUpgradeCost * (clickerManager.getClickCount() / 600);
            
            clickLevelText.text ="" + clickerManager.clickLevel;
            clickUpgradeCostText.text = "" + clickUpgradeCost + " G";
            clickRevenueText.text = "" + clickerManager.clickRevenue + " G";
            _ShowMoney(money);
        }
    }

    // Called when enabling the menu
    public void ToggleMenu()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);
        }
    }


    // TODO : Set all panel to false as to only have one enable // TODO : Rationalize all of the code to make it shorter and more beautiful
    public void onClickCategory(Button button)
    {
        // Check which button has been clicked 
        if (button.name == "UpgradeCategoryButton") {
            if (UpgradePanel.activeSelf)
            {
                // If the menu pannel is off then open it, if not the close it
                if (menuPanel.activeSelf)
                {
                    menuPanel.SetActive(false);
                } else
                {
                    menuPanel.SetActive(true) ;
                }
            }else
            {
                menuPanel.SetActive(true);
                UpgradePanel.SetActive(true);
                BuyPanel.SetActive(false);
            }
        }
        if (button.name == "BuyCategoryButton")
        {
            if (BuyPanel.activeSelf)
            {
                if (menuPanel.activeSelf)
                {
                    menuPanel.SetActive(false);
                }
                else
                {
                    menuPanel.SetActive(true);
                }
            }
            else
            {
                menuPanel.SetActive(true);
                BuyPanel.SetActive(true);
                UpgradePanel.SetActive(false);
            }
        }
    }


    // Called when the player clicks the screen
    public void OnClick()
    {
        clickerManager.OnClick();

        // Update the UI text object
        _ShowMoney(money);
    }

    public void getMoney(int amount)
    {
        money += amount;
    }


    //Function to update the text
    private void _ShowMoney(int money)
    {
        clickCountText.text = "Gold: " + _getTextFromInt(money);
    }

    private string _getTextFromInt(int value)
    {
        string[] list = {"", "K", "M", "B", "T", "Q", "Quint", "Sixt" };
        string result = "";
        string tmp = value.ToString();
        //Debug.Log(tmp.Length/4);
        if (tmp.Length / 4 == 0)
        {
            result= tmp;
        } else
        {
            result = tmp.Substring(0, (tmp.Length % 4) + 1) + " " + list[tmp.Length / 4];
        }
        return result ;
    }
}
