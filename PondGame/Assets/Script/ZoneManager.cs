using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public Zone zone;
    public List<Zone> list_Zone = new List<Zone> ();

    // Start is called before the first frame update
    void Start()
    {
        list_Zone.Add (zone);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyFish(int zoneId, int fishId)
    {
        list_Zone[zoneId].buyFish(fishId);
    }


}
