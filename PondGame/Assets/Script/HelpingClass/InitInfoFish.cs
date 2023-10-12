using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInfoFish : MonoBehaviour
{
    // INFO FOR initInfoFish
    public int fishId;
    public string fishName;
    public string fishDescription;
    public string fishShortDescription;
    public Sprite fishSprite;

    public bool isBlocked = false;
    public GameObject blockObject;

    public void initInfoFish(int fishId, string fishName, string fishDescription, string fishShortDescription, Sprite fishSprite, bool isBlocked, GameObject blockObject = null)
    {
        this.fishId = fishId;
        this.fishName = fishName;
        this.fishDescription = fishDescription;
        this.fishShortDescription = fishShortDescription;
        this.fishSprite = fishSprite;
        this.isBlocked = isBlocked;
        this.blockObject = blockObject;
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
