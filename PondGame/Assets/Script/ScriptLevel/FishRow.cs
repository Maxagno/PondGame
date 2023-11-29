using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishRow : MonoBehaviour
{
    [SerializeField]
    public string id;

    public FishLevel fishlevel;
    public FishRowManager fishRowManager;

    public string fishId;
    public string zoneId;

    public string fishName;
    public string fishDescription;

    public AmountMoney production;
    public AmountMoney cost;
    public AmountMoney level;

    public AmountMoney tmp_production;
    public AmountMoney tmp_cost;

    public TMP_Text Name_Text;
    public TMP_Text Level_Text;
    public TMP_Text Production_Text;
    public TMP_Text Cost_Text;

    public Button buyButton;

    public int amountLvlUp = 1;

    // Start is called before the first frame update
    void Start()
    {
        initFish();
    }
    public void setInfo(FishLevel fish, FishRowManager fishRowManager)
    {
        this.fishRowManager = fishRowManager;
        this.fishlevel = fish;
        updateInfo();
    }
    public void onClickUpgrade()
    {
        if (fishRowManager.updateBoughtMoney(cost) == 0)
        {
            double amount = fishlevel.levelUp(amountLvlUp);
            fishRowManager.updateProduction(amount);
            updateInfo();
        }
    }

    public void setAmount(int amount)
    {
        amountLvlUp = amount;
        updateInfo();
    }

    public void updateInfo()
    {
        AmountMoney tmp_level = new AmountMoney(amountLvlUp);
        cost = fishlevel.getInfoToLevel(amountLvlUp);
        production = fishlevel.getTotalProduction();
        level = fishlevel.getLevel();

        tmp_production = fishlevel.getfutureProduction();

        Cost_Text.text = cost.ToString();
        Production_Text.text = production.ToString() + " -> " + tmp_production.ToString();
        Level_Text.text = level.ToString() + " + " + tmp_level.ToString();
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
