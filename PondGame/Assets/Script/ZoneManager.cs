using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public List<GameObject> list_Zone = new List<GameObject> ();
    public List<GameObject> list_PrefabZone = new List<GameObject> ();
    public GameObject StartZone;
    public int numberOfZone = 4;

    private float last_y = 0;

    // Start is called before the first frame update
    void Start()
    {
        //initZoneManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newFish(int fishId, int zoneId)
    {
        Debug.Log("Here in the Zone manager IdZone:  " + zoneId + "enabling the fishId : " + fishId);
        list_Zone[zoneId].GetComponent<Zone>().buyFish(fishId);
        
    }

    public List<InitInfo> initialiseZoneManager(int amountOfZone = 4)
    {
        List<InitInfo> result = new List<InitInfo>();
        for (int i = 0; i < numberOfZone; i++)
        {
            // Init the zone
            GameObject zoneTMP = Instantiate(list_PrefabZone[0]);
            Zone zone = zoneTMP.GetComponent<Zone>();
            InitInfo temporaryVar = zone.initZone(i, amountOfZone);
            temporaryVar.zone = zoneTMP;
            // Init position
            zoneTMP.transform.position = new Vector3(0, last_y, 0);
            if(i > 0)
            {
                zoneTMP.SetActive(false);
            }
            list_Zone.Add(zoneTMP);
            last_y += -17;
            result.Add(temporaryVar);
        }
        return result;
    }


}
