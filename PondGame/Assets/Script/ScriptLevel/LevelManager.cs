using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour, IDataPersistence
{

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


    public List<GameObject> listBoostRow_GameObject = new List<GameObject>();
    public List<BoostLevel> listBoostRow = new List<BoostLevel>();
    public List<FishLevel> listFish = new List<FishLevel>();

    public FishRowManager fishRowManager;

    // UI TEXT Variable
    public TMP_Text MoneyText;
    public TMP_Text ProductionText;

    public void LoadData(GameData data)
    {
        topLimit = 1f;
        bottomLimit = -2000f;

        money = new AmountMoney(0.00D);

        //Debug.Log("Here in the loading of the level manager, data.money is : " + data.money);
        this.money.amount = data.money;
        cam.transform.position = new Vector3(cam.transform.position.x, data.cameraPositionY, cam.transform.position.z);
        clicker = new ClickerLevel();
        Debug.Log(clicker.getLevel());
        moneyUsed = new AmountMoney(0.00D);
        production = new AmountMoney(0.00D);

        initFishRowManager();
        updateTextMoney();
        updateTextProduction();

    }

    public void SaveData(ref GameData data)
    {
        data.money = this.money.amount;
        data.cameraPositionY = cam.transform.position.y;
    }

    

    public LevelManager(LevelManager levelManager)
    {

    }

    private void initFishRowManager()
    {
        fishRowManager.setInfo(this, clicker, listFish);
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
        
        //Debug.Log("Updating the money");

        //Debug.Log("Money = " + money.ToString());
        //Debug.Log("amount added = " + amount.ToString());
        //Debug.Log("Money.listgold[i] : " + money.listGold[0]);

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

    public void boostUpgrade(int zoneId, int fishId, double value)
    {
        for (global::System.Int32 i = 0; i < listFish.Count; i++)
        {
            FishLevel tmpFish = listFish[i];
            if (tmpFish.zoneId == zoneId || zoneId == -1)
            {
                if (fishId == tmpFish.id || fishId == -1)
                {
                    updateProduction(tmpFish.addBoost(value));
                    
                }
            }
        }
        fishRowManager.boostUpgrade();
    }

    public void hideRow(int id)
    {
        listBoostRow_GameObject[id].SetActive(false);
    }
    // PRIVATE FUNCTION 



    private void UpdateCanBeBought()
    {/*
        if (clickRow != null)
        {
            if (clickRow.cost.amount.CompareTo(money.getAmount()) > 0)
            {
                clickRow.buyButton.interactable = false;
            }
            else
            {
                clickRow.buyButton.interactable = true;
            }
        }
        for (int i = 0; i < listFishRow.Count; i++)
        {
            if (listFishRow[i] != null)
            {
                AmountMoney tmp_Cost = listFishRow[i].cost;
                if (tmp_Cost.amount.CompareTo(money.getAmount()) > 0)
                {
                    listFishRow[i].buyButton.interactable = false;
                }
                else
                {
                    listFishRow[i].buyButton.interactable = true;
                }
            }
        }
        for(int i = 0; i < listBoostRow.Count; i++)
        {
            if (listBoostRow[i] != null)
            {
                AmountMoney tmp_Cost = listBoostRow[i].cost;
                if (tmp_Cost.amount.CompareTo(money.getAmount()) > 0)
                {
                    listBoostRow[i].buyButton.interactable = false;
                }
                else
                {
                    listBoostRow[i].buyButton.interactable = true;
                }
            }
        }*/
    }

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
            Debug.Log(_curPosition);
            _velocity = _curPosition - _prevPosition;

        }
        if (Input.GetMouseButtonUp(0))
        {
            _underInertia = true;

        }
    }
}
