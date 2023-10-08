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

    public void initZoneManager()
    {
        for (int i = 0; i < numberOfZone; i++)
        {
            GameObject zoneTMP = Instantiate(list_PrefabZone[0]);
            Zone zone = zoneTMP.GetComponent<Zone>();
            zone.initZone(4);
            zoneTMP.transform.position = new Vector3(0, last_y, 0);
            Renderer rend = zoneTMP.GetComponent<Renderer>();
            list_Zone.Add(zoneTMP);
            last_y += -17;
        }
    }

    public void newFish(int fishId, int zoneId)
    {
        Debug.Log("Here in the Zone manager IdZone:  " + zoneId);
        list_Zone[zoneId - 1].GetComponent<Zone>().buyFish(fishId);
        
    }

    public List<InitInfo> initialiseZoneManager()
    {
        List<InitInfo> result = new List<InitInfo>();
        for (int i = 0; i < numberOfZone; i++)
        {
            InitInfo temporaryVar = new InitInfo();
            GameObject zoneTMP = Instantiate(list_PrefabZone[0]);
            Zone zone = zoneTMP.GetComponent<Zone>();
            List<GameObject> blockObjectZone = zone.initZone(4);
            temporaryVar.initInfoZone(i, 4, zone.name);
            // Adding the link between each block and the id of the fish that will be spawned
            List<(int, int)> listLinkBlock2Fish = new List<(int, int)>();
            for (int j = 0; j < blockObjectZone.Count && j < 4; j++)
            {
                doubleInt temp = new doubleInt();
                temp.init(j, j);
                temporaryVar.listOfLink.Add(temp);
            }
            zoneTMP.transform.position = new Vector3(0, last_y, 0);
            Renderer rend = zoneTMP.GetComponent<Renderer>();
            list_Zone.Add(zoneTMP);
            last_y += -17;
            result.Add(temporaryVar);
        }
        return result;
    }


}
