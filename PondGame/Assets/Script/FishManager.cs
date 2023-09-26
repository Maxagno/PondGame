using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public int id;
    public string name;
    public GameObject _prefabFish;
    private List<GameObject> list_fish = new List<GameObject>();

    private List<Fish> listValueFish = new List<Fish>();
    private List<int> listID = new List<int>();

    public void initFish()
    {        
        listValueFish.Add(new Fish(0, "Test"));
        buyFish(0);
    }
    
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
        GameObject fishTMP = Instantiate(_prefabFish);
        Fish tmp_Fish = listValueFish[id];
        Fish fish = fishTMP.GetComponent<Fish>();
        fish.name = tmp_Fish.name;
        fishTMP.transform.position = new Vector3(0,0,0);
        list_fish.Add(fishTMP);
    }
}
