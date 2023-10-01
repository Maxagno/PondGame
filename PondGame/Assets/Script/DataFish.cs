using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFish : MonoBehaviour
{

    public int id;
    public string name;
    public string description;

    public int level;
    public int production;
    public int cost;

    public DataFish(int new_id, string new_name, string new_description, int new_production, int new_cost, int new_level)
    {
        id = new_id;
        name = new_name;
        description = new_description;
        
        level = new_level;
        Debug.Log(level);
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
