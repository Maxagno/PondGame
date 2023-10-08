using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public string name;
    public string description;
    public FishManager fishManager;
    public List<Sprite> listSprite = new List<Sprite>();
    public List<GameObject> listBlock = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> initZone(int amount)
    {
        fishManager.initFishManager(amount, listSprite);
        return listBlock;
    }

    public void buyFish(int id)
    {
        fishManager.buyFish(id);
    }
}
