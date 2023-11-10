using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    Vector3 touchStart;
    private float topLimit = 0f;
    private float bottomLimit = -2000.0f;

    private Vector3 _curPosition;
    private Vector3 _velocity;
    private bool _underInertia;
    private float _time = 0.0f;
    public float SmoothTime = 2;

    public Vector3 direction;

    public static LevelManager instance;

    public GameObject PanelManager;
    public GameObject ZoneManager;

    private bool hasInitialized = false;


    private float timeSinceLastResourceGeneration = 0f;
    private int timeCalled = 0;

    private AmountMoney money;
    private AmountMoney moneyUsed;
    public AmountMoney production;

    private ZoneManager zoneManager;
    private ClickerLevel clicker;
    private PanelManager panelManager;

    public List<GameObject> listRow_GameObject = new List<GameObject>();
    public List<FishRow> listFishRow = new List<FishRow>();
    public ClickRow clickRow;
    public List<FishLevel> listFish = new List<FishLevel>();


    // UI TEXT Variable
    public TMP_Text MoneyText;
    public TMP_Text ProductionText;

    void Start()
    {
        money = new AmountMoney(0, "");
        moneyUsed = new AmountMoney(0, "");
        production = new AmountMoney(0, "");
        clicker = new ClickerLevel();
        clickRow.setClicker(clicker);
        Awake();
        updateTextMoney();
        updateTextProduction();
    }

    void Update()
    {
        if (!PanelManager.activeSelf)
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


            Debug.Log("Call ressourceGeneration " + timeCalled);
            //money.updateAllAmount(production);
            updateMoney(production);
        }
    }


    public void onClick()
    {
        if (!PanelManager.activeSelf)
        {
            money.updateAllAmount(clicker.getTotalProduction());
            updateTextMoney();
        }
    }

    public AmountMoney getMoney()
    {
        return money;
    }

    public void updateProduction(AmountMoney amount)
    {
        production.updateAllAmount(amount);
        updateTextProduction();
    }


    public void updateMoney(AmountMoney amount = null)
    {
        
        //Debug.Log("Updating the money");

        //Debug.Log("Money = " + money.ToString());
        //Debug.Log("amount added = " + amount.ToString());
        //Debug.Log("Money.listgold[i] : " + money.listGold[0]);

        money.updateAllAmount(amount);
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
            if ( amount.compareIsInfAmount(money) )
            {
                money.substractAllAmount(amount);
                updateTextMoney();
                return 0;
            }
        }
        return 1;
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

    private void UpdateCanBeBought()
    {
        if (! clicker.total_cost.compareIsInfAmount(money))
        {
            clickRow.buyButton.interactable = false;
        }
        else
        {
            clickRow.buyButton.interactable = true;
        }
        for (int i = 0; i < listFish.Count; i++)
        {
            FishLevel tmpFish = listFish[i];
            AmountMoney tmp_Cost = tmpFish.getCost();
            if (! tmp_Cost.compareIsInfAmount(money))
            {
                listFishRow[i].buyButton.interactable = false;
            } else
            {
                listFishRow[i].buyButton.interactable = true;
            }
        }
    }

    private void updateTextMoney()
    {
        UpdateCanBeBought();
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
            //Debug.Log("New: " + finalYPos);
            Vector3 desiredPosition = new Vector3(cam.transform.position.x, finalYPos, cam.transform.position.z);
            cam.transform.position = desiredPosition;

            _curPosition = desiredPosition;
            _velocity = _curPosition - _prevPosition;

        }
        if (Input.GetMouseButtonUp(0))
        {
            _underInertia = true;

        }
    }
}
