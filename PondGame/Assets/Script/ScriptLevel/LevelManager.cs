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
    public GameObject Clicker;
    public GameObject ZoneManager;

    private bool hasInitialized = false;


    private float timeSinceLastResourceGeneration = 0f;
    private int timeCalled = 0;

    private AmountMoney money;
    private AmountMoney moneyUsed = new AmountMoney(0, "");
    public AmountMoney production;

    private ZoneManager zoneManager;
    private Clicker clicker;
    private PanelManager panelManager;


    // UI TEXT Variable
    public TMP_Text MoneyText;
    public TMP_Text ProductionText;

    void Start()
    {
        Awake();
        updateTextMoney();
        updateTextProduction();
    }


    // Start is called before the first frame update
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

    

    private void updateTextMoney()
    {
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
