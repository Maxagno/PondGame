using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishRow : MonoBehaviour
{
    [SerializeField]
    public int id;

    public FishLevel fishlevel;
    public LevelManager levelManager;

    public int fishId;
    public int zoneId;

    public string fishName;
    public string fishDescription;

    public AmountMoney production;
    public AmountMoney cost;
    public AmountMoney level;

    public TMP_Text Name_Text;
    public TMP_Text Level_Text;
    public TMP_Text Production_Text;
    public TMP_Text Cost_Text;

    public Button buyButton;

    public bool isLocked = true;

    public float timeSinceLastPressed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initFish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickUpgrade()
    {
        if (levelManager.updateBoughtMoney(cost) == 0)
        {
            AmountMoney prodUpgrade = fishlevel.levelUp(1);
            levelManager.updateProduction(prodUpgrade);
            updateInfo();
        }
    }

    private void updateInfo()
    {
        cost = fishlevel.getCost();
        production = fishlevel.getTotalProduction();
        level = fishlevel.getLevel();
        Cost_Text.text = cost.ToString();
        Production_Text.text = production.ToString();
        Level_Text.text = level.ToString();
    }

    private void initFish()
    {
        if (fishlevel == null)
        {
            return;
        }
        fishId = fishlevel.id;
        fishName = fishlevel.getName();
        Name_Text.text = fishName;
        updateInfo();
    }

}
