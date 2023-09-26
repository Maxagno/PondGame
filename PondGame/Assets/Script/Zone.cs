using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public string name;
    public string description;
    public FishManager fishManager;
    
    // Start is called before the first frame update
    void Start()
    {
        fishManager.initFish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyFish(int id)
    {
        fishManager.buyFish(id);
    }
}
