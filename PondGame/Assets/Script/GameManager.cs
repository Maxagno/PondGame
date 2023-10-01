using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject menuPanel;
    public GameObject Clicker;
    public GameObject ZoneManager;

    private bool hasInitialized = false;


    private float timeSinceLastResourceGeneration = 0f;
    private int timeCalled = 0;

    private int money = 0;
    private int moneyUsed = 0;
    public int production = 0;

    private ZoneManager zoneManager;
    private Clicker clicker;


    // UI TEXT Variable
    public TMP_Text MoneyText;
    public TMP_Text ProductionText;

    void Update()
    {
        if (! menuPanel.activeSelf)
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
            timeSinceLastResourceGeneration= 0f;
            //TODO 
            money += production;
            updateMoney();
        }
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
        zoneManager = ZoneManager.GetComponent<ZoneManager>();
        clicker = Clicker.GetComponent<Clicker>();
        Awake();
        updateTextMoney();
        updateTextProduction();
    }

    public int getMoney()
    {
        return money;
    }

    public void updateProduction(int amount)
    {
        production = amount;
        updateTextProduction();
    }

    public void newFish(int id, int zoneId)
    {
        zoneManager.newFish(id, zoneId);
    }

    public void updateMoney(int amount = 0)
    {
        money += amount;
        updateTextMoney();
    }
    
    public (ShopRow, int) upgradeClick(int amount, ShopRow clickRow)
    {
        return clicker.upgradeClick(amount, clickRow);
    }

    private void updateTextMoney()
    {
        MoneyText.text = "Gold: " + _getTextFromInt(money);
    }
    
    private void updateTextProduction()
    {
        ProductionText.text = "Gold: " + _getTextFromInt(production) + "/s";
    }
    //Do better
    private string _getTextFromInt(int value)
    {
        string[] list = { "", "K", "M", "B", "T", "Q", "Quint", "Sixt" };
        string result = "";
        string tmp = value.ToString();
        //Debug.Log(tmp.Length/4);
        if (tmp.Length / 4 == 0)
        {
            result = tmp;
        }
        else
        {
            result = tmp.Substring(0, (tmp.Length % 4) + 1) + " " + list[tmp.Length / 4];
        }
        return result;
    }

    // TEST CAMERA Movement 

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
