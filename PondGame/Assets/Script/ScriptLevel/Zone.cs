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

    public void buyFish(int id)
    {
        listFish[id].SetActive(true);
    }

    public void RemoveBlock(int id)
    {
        listBlock[id].SetActive(false);
    }
    /*
    public void buyFish(int id)
    {
        Debug.Log("" + id);
        GameObject fishTMP = Instantiate(_prefabFish);
        Fish fish = fishTMP.GetComponent<Fish>();
        fish.setImg(listSprite[id]);
        fishTMP.transform.position = spawnPoint[id].transform.position;
        Debug.Log(spawnPoint[id].name);

        list_fish.Add(fishTMP);
    }*/

}
