using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    public string name;
    public string description;


    // Private Attribute 

    public float moveSpeed = 5f;
    public float maxDistance_X = 3.8f;
    public float minDistance_X = -3.8f;
    public int maxDistance_Y = 2;

    private bool toLeft = false;

    public int level = 1;
    public int baseProduction = 10;



    public Fish(int new_id, string new_name, string new_description = "", int new_baseProduction = 10)
    {
        id = new_id;
        name = new_name;
        description = new_description;
        baseProduction = new_baseProduction;
        level = 1;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        
       MoveFish(step);
        
    }

    

    /*_____________________________________________________________________________________________*/
    // Private method 

    private void MoveFish(float step)
    {
        if (toLeft)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-4, transform.position.y), step);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(4, transform.position.y), step);
        }
        TurnBack();
    }

    private void TurnBack()
    {
        bool tmp = toLeft;
        if (toLeft)
        {
            toLeft = !(transform.position.x < minDistance_X);
        } else {
            toLeft = (transform.position.x > maxDistance_X);
        }
        if (tmp != toLeft)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    // Increase in the production

    private void production_Increase()
    {
        baseProduction = baseProduction + level;
    }


    /*_____________________________________________________________________________________________*/
    // GET SET METHOD

    public int get_level() { return level; }

    public void set_level(int amount) {  this.level += amount; }

    public int get_production() { return baseProduction; }

    public string getName()
    {
        return name;
    }


}
