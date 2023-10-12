using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    public string name;
    public string description;
    public string shortDescription;

    public Sprite image;
    public SpriteRenderer sprite;

    // Private Attribute 

    public float moveSpeed = 5f;
    public float maxDistance_X = 3.8f;
    public float minDistance_X = -3.8f;
    public int maxDistance_Y = 2;

    private bool toLeft = false;



    public Fish(int new_id, string new_name, Sprite img, string new_description, string shortdescription)
    {
        id = new_id;
        name = new_name;
        description = new_description;
        shortDescription = shortdescription;
        if (img != null)
        {
            sprite.sprite = img;
        }
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4, transform.position.y, -2), step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(4, transform.position.y, -2), step);
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



    public int getId()
    {
        return id;
    }

    public void setImg(Sprite image)
    {
        sprite.sprite = image;
    }

}
