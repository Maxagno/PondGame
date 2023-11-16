using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelManager : MonoBehaviour
{

    public GameObject UpgradePanel;
    public GameObject BuyCategory;
    public GameObject MenuPanel;

    // GESTION OF THE PANEL


    // Called when enabling the menu
    public void ToggleMenu()
    {
        if (MenuPanel.activeSelf)
        {
            MenuPanel.SetActive(false);
        }
        else
        {
            MenuPanel.SetActive(true);
        }
    }


    // TODO : Set all panel to false as to only have one enable // TODO : Rationalize all of the code to make it shorter and more beautiful
    public void onClickCategory(Button button)
    {
        // Check which button has been clicked 
        if (button.name == "UpgradeCategoryButton")
        {
            if (UpgradePanel.activeSelf)
            {
                // If the menu pannel is off then open it, if not the close it
                if (MenuPanel.activeSelf)
                {
                    MenuPanel.SetActive(false);
                }
                else
                {
                    MenuPanel.SetActive(true);
                }
            }
            else
            {
                MenuPanel.SetActive(true);
                UpgradePanel.SetActive(true);
                BuyCategory.SetActive(false);
            }
        }
        if (button.name == "BuyCategoryButton")
        {
            if (BuyCategory.activeSelf)
            {
                if (MenuPanel.activeSelf)
                {
                    MenuPanel.SetActive(false);
                }
                else
                {
                    MenuPanel.SetActive(true);
                }
            }
            else
            {
                MenuPanel.SetActive(true);
                BuyCategory.SetActive(true);
                UpgradePanel.SetActive(false);
            }
        }
    }
}
