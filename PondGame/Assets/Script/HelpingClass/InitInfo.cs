using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInfo : MonoBehaviour
{
    // INFO FOR InitInfoZone
    public int zoneId;
    public string zoneName;
    public List<InitInfoFish> listInfoFish;

    // INFO FOR InitInfoRow

    public int rowId;
    //public List<InitInfoFish> infoFish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initInfoRow(int zoneId, int rowId)
    {
        this.zoneId = zoneId;
        this.rowId = rowId;
        listInfoFish = new List<InitInfoFish>();

    }

    public void initInfoZone(int zoneId, string zoneName)
    {
        this.zoneId = zoneId;
        this.zoneName = zoneName;
        listInfoFish = new List<InitInfoFish>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
