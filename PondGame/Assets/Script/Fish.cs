using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    public int id;
    public string name;
    public string description;
    public int baseProduction;

    public  Fish(int new_id, string new_name, string new_description, int new_baseProduction)
    {
        id = new_id;
        name = new_name;
        description = new_description;
        baseProduction = new_baseProduction;
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
