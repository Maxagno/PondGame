using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFish : MonoBehaviour
{

    public int id;
    public string name;
    public string description;

    public int level;
    public AmountMoney production;
    public AmountMoney cost;

    public DataFish(int new_id, string new_name, string new_description, AmountMoney new_production, AmountMoney new_cost, int new_level = 0)
    {
        id = new_id;
        name = new_name;
        description = new_description;
        
        level = new_level;
        production = new_production;
        cost = new_cost;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
