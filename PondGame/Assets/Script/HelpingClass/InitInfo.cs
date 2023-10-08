using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInfo : MonoBehaviour
{
    public int zoneId;
    public string zoneName;
    public int fishId;
    public int rowId;
    public int cost;
    public List<doubleInt> listOfLink;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initInfoRow(int zoneId, int fishId, int cost, int rowId)
    {
        this.zoneId = zoneId;
        this.fishId = fishId;
        this.cost = cost;
        this.rowId = rowId;
    }

    public void initInfoZone(int zoneId, int fishId, string zoneName)
    {
        this.zoneId = zoneId;
        this.fishId = fishId;
        this.zoneName = zoneName;
        listOfLink = new List<doubleInt> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
