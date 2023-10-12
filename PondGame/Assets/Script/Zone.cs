using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public string name;
    public string description;

    // Add a thumbnail for each Zone

    public List<GameObject> listBlock = new List<GameObject>();

    public List<GameObject> listFish = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InitInfo initZone(int amount)
    {
        InitInfo resultInit = new InitInfo();
        resultInit.initInfoZone(id, name);

        List<InitInfoFish> listInfoFish = resultInit.listInfoFish;

        // Init the loop for list of fish
        for (int i = 0; i < listInfoFish.Count;i++)
        {
            Fish fishTMP = listInfoFish[i].GetComponent<Fish>();
            InitInfoFish infoFishTMP = new InitInfoFish();
            infoFishTMP.initInfoFish(fishTMP.id, fishTMP.name, fishTMP.description, fishTMP.shortDescription, fishTMP.image, true, listBlock[i]);
            listInfoFish.Add(infoFishTMP);
        }
        
        resultInit.listInfoFish = listInfoFish;

        // Add a random selection of the fish to be added to the zone
        return resultInit;
    }

    public void buyFish(int id)
    {
        listFish[id].SetActive(true);
    }

    public void RemoveBlock(int id)
    {
        listBlock[id].SetActive(false);
    }

}
