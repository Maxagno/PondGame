using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    [Header("Scène Name")]

    [SerializeField] public string sceneName;
    [SerializeField]
    private Camera cam;

    Vector3 touchStart;
    private float topLimit;
    private float bottomLimit;

    private Vector3 _curPosition;
    private Vector3 _velocity;
    private bool _underInertia;
    private float _time = 0.0f;
    public float SmoothTime = 2;

    public Vector3 direction;


    public GameObject UIManager;
    public GameObject ZoneManager;

    

    private float timeSinceLastResourceGeneration = 0f;
    private int timeCalled = 0;

    private AmountMoney money;

    private AmountMoney moneyUsed;
    public AmountMoney production;

    public int amountLvlUp = 1;

    public ClickerLevel clicker;

    public List<Button> listRow_Amount = new List<Button>();

    public List<BoostLevel> listBoostRow = new List<BoostLevel>();
    public List<FishLevel> listFish = new List<FishLevel>();

    public FishRowManager fishRowManager;
    public BoostRowManager boostRowManager;

    // UI TEXT Variable
    public TMP_Text MoneyText;
    public TMP_Text ProductionText;

    public void LoadData(GameData data)
    {
        SceneData dataScene = data.listOfScene[sceneName];
        topLimit = 1f;
        bottomLimit = -2000f;

        money = new AmountMoney(0.00D);

        //Debug.Log("Here in the loading of the level manager, data.money is : " + data.money);
        this.money.amount = dataScene.money;
        cam.transform.position = new Vector3(cam.transform.position.x, dataScene.cameraPositionY, cam.transform.position.z);
        clicker = new ClickerLevel();
        moneyUsed = new AmountMoney(0.00D);
        production = new AmountMoney(0.00D);
        this.production.amount = dataScene.production;
        for (global::System.Int32 i = 0; i < dataScene.listOfFish.Count; i++)
        {
            loadDataFish(listFish[i], dataScene.listOfFish[i]);
        }
        Debug.Log("Count boost data : " + dataScene.listOfBoost.Count);
        //listBoostRow.Clear();
        for (global::System.Int32 i = 0; i < dataScene.listOfBoost.Count; i++)
        {
            if (dataScene.listOfBoost[i] != null)
            {
                if (listBoostRow.Count <= i)
                {
                    listBoostRow.Add(new BoostLevel());
                }
                loadDataBoost(listBoostRow[i], dataScene.listOfBoost[i]);
                listBoostRow[i].levelManager = this;
            }
        }
        if (dataScene.clicker != null)
        {
            loadClicker(dataScene.clicker);
        }
        initFishRowManager();
        initBoostRowManager();
        updateTextMoney();
        updateTextProduction();

    }

    private void loadClicker(ClickerData data)
    {
        clicker.level.amount = data.level;
        clicker.total_cost.amount = data.currentCost;
        clicker.base_cost.amount = data.baseCost;
        clicker.total_Production.amount = data.currentProduction;
        clicker.base_Production.amount = data.baseProduction;
        clicker.boostAmount = data.boostAmount;
        clicker.base_boostAmount = data.base_boostAmount;
        clicker.incremental_CostUpgrade = data.incremental_CostUpgrade;
    }

    private void saveClicker(ClickerData data) 
    {
        data.level = clicker.level.amount;
        data.currentCost = clicker.total_cost.amount;
        data.baseCost = clicker.base_cost.amount;
        data.currentProduction = clicker.total_Production.amount;
        data.baseProduction = clicker.base_Production.amount;
        data.boostAmount = clicker.boostAmount;
        data.base_boostAmount = clicker.base_boostAmount;
        data.incremental_CostUpgrade = clicker.incremental_CostUpgrade;
    }

    private void saveDataFish(FishLevel fish, FishData fishData)
    {
        fishData.sceneId = "SeaLevel";
        fishData.zoneId = fish.zoneId;
        fishData.id = fish.id;

        fishData.level = fish.level.amount;
        fishData.currentCost = fish.total_cost.amount;
        fishData.baseCost = fish.base_cost.amount;

        fishData.currentProduction = fish.total_Production.amount;
        fishData.baseProduction = fish.base_Production.amount;

        fishData.boostAmount = fish.boostAmount;
        fishData.base_boostAmount = fish.base_boostAmount;

        fishData.incremental_CostUpgrade = fish.incremental_CostUpgrade;
    } 

    private void loadDataFish(FishLevel fish, FishData fishData)
    {
        fish.level.amount = fishData.level;
        fish.total_cost.amount = fishData.currentCost;
        fish.base_cost.amount = fishData.baseCost;

        fish.total_Production.amount = fishData.currentProduction;
        fish.base_Production.amount = fishData.baseProduction;

        fish.boostAmount = fishData.boostAmount;
        fish.base_boostAmount = fishData.base_boostAmount;

        fish.incremental_CostUpgrade = fishData.incremental_CostUpgrade;
    }

    private void saveDataBoost(BoostLevel boost, BoostData boostData)
    {
        boostData.copyData(boost);
    }

    private void loadDataBoost(BoostLevel boost, BoostData boostData)
    {
        boost.loadData(boostData);
    }

    public void SaveData(ref GameData data)
    {
        SceneData dataScene = data.listOfScene[sceneName];
        dataScene.money = this.money.amount;
        dataScene.cameraPositionY = cam.transform.position.y;
        dataScene.production = this.production.amount;
        for (global::System.Int32 i = 0; i < listFish.Count; i++)
        {
            if (i >= dataScene.listOfFish.Count)
            {
                dataScene.listOfFish.Add(new FishData());
            }
            saveDataFish(listFish[i], dataScene.listOfFish[i]);
        }
        dataScene.listOfBoost.Clear();
        for (global::System.Int32 i = 0; i < listBoostRow.Count; i++)
        {
            if (i >= dataScene.listOfBoost.Count)
            {
                dataScene.listOfBoost.Add(new BoostData());
            }
            if (listBoostRow[i] != null)
            {
                saveDataBoost(listBoostRow[i], dataScene.listOfBoost[i]);
            }
        }
        if (dataScene.clicker == null)
        {
            dataScene.clicker = new ClickerData(this.sceneName, clicker.level.amount, clicker.total_cost.amount, clicker.base_cost.amount, clicker.total_Production.amount, clicker.base_Production.amount, clicker.boostAmount, clicker.base_boostAmount, clicker.incremental_CostUpgrade);
        } else
        {
            saveClicker(dataScene.clicker);
        }

    }


    private void initFishRowManager()
    {
        fishRowManager.setInfo(this, clicker, listFish);
    }
    private void initBoostRowManager()
    {
        boostRowManager.setInfo(this, listBoostRow);
    }


    void Update()
    {
        if (! UIManager.activeSelf)
        {
            PanCamera();
        }
        if (_underInertia && _time <= SmoothTime)
        {
            cam.transform.position += _velocity;
            float newY = Mathf.Clamp(cam.transform.position.y, bottomLimit, topLimit);
            cam.transform.position = new Vector3(cam.transform.position.x, newY, cam.transform.position.z);
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, _time);
            _time += Time.smoothDeltaTime;
        }
        else
        {
            _underInertia = false;
            _time = 0.0f;
        }
        // Calculate the current resource generation rate per second based on the current upgrade level
        //float resourceGenerationRate = GetResourceGenerationRate(upgradeLevel);

        // Add the generated resources to the resources field every second
        timeSinceLastResourceGeneration += Time.deltaTime;
        if (timeSinceLastResourceGeneration >= 1f)
        {
            timeCalled++;
            //Debug.Log("Call ressourceGeneration " + timeCalled);
            timeSinceLastResourceGeneration = 0f;
            //TODO 


            //Debug.Log("Call ressourceGeneration " + timeCalled);
            updateMoney(production);
        }
    }


    public void updateAmount(int  amount)
    {
        if (amount == 1)
        {
            listRow_Amount[0].interactable = false;
            listRow_Amount[1].interactable = true;
            listRow_Amount[2].interactable = true;

        }
        else if (amount == 10)
        {
            listRow_Amount[0].interactable = true;
            listRow_Amount[1].interactable = false;
            listRow_Amount[2].interactable = true;

        }
        else
        {
            listRow_Amount[0].interactable = true;
            listRow_Amount[1].interactable = true;
            listRow_Amount[2].interactable = false;
        }
        amountLvlUp = amount;
        fishRowManager.updateAmount(amount);
    }

    public void onClick()
    {
        if (!UIManager.activeSelf)
        {
            money.updateAmount(clicker.getTotalProduction().getAmount());
            updateTextMoney();
        }
    }

    public AmountMoney getMoney()
    {
        return money;
    }

    public void updateProduction(double amount)
    {
        production.updateAmount(amount);
        updateTextProduction();
    }


    public void updateMoney(AmountMoney amount = null)
    {
        money.updateAmount(amount.getAmount());
        updateTextMoney();
    }

    /*
     * updateBoughtMoney : Update the current amount of money with the cost of upgrade bought
     * It checks if amount exist and if it's inf or equal to the current amount of money
     * If it run as expected then it return 0
     * If not the it return 1
     */
    public int updateBoughtMoney(AmountMoney amount = null)
    {
        if (amount != null)
        {
            if ( amount.amount.CompareTo(money.amount) < 1 )
            {
                money.amount = money.amount - amount.getAmount();
                updateTextMoney();
                return 0;
            }
        }
        return 1;
    }

    public void boostUpgrade(string zoneId, string fishId, double value)
    {

        for (global::System.Int32 i = 0; i < listFish.Count; i++)
        {
            FishLevel tmpFish = listFish[i];
            if (string.Equals(tmpFish.zoneId,zoneId) || string.Equals(zoneId,"-1"))
            {
                if (string.Equals(fishId,tmpFish.id) || string.Equals(fishId,"-1"))
                {
                    updateProduction(tmpFish.addBoost(value));
                    
                }
            }
        }
        fishRowManager.boostUpgrade();
    }

    public void hideRow(int id)
    {
        for(int i = 0;i < listBoostRow.Count;i++)
        {
            if (listBoostRow[i].id == id)
            {
                listBoostRow.RemoveAt(i);
            }
        }
        boostRowManager.hideRow(id);
    }
    // PRIVATE FUNCTION 

    private void updateTextMoney()
    {
        fishRowManager.UpdateCanBeBought();
        MoneyText.text = "Gold: " + money.ToString();
    }

    private void updateTextProduction()
    {
        ProductionText.text = "Gold: " + production.ToString() + "/s";
    }


    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            _underInertia = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 _prevPosition = _curPosition;

            direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            float finalYPos = cam.transform.position.y + direction.y;
            //Debug.Log("Old: " + finalYPos);
            finalYPos = Mathf.Clamp(finalYPos, bottomLimit, topLimit);
            //Debug.Log("New: " + finalYPos + "Min : " + bottomLimit + " Max : "+topLimit);
            cam.transform.position = new Vector3(cam.transform.position.x, finalYPos, cam.transform.position.z);

            _curPosition = cam.transform.position;
            _velocity = _curPosition - _prevPosition;

        }
        if (Input.GetMouseButtonUp(0))
        {
            _underInertia = true;

        }
    }
}
