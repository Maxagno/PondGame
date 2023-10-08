using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public int id;
    public string name;
    public GameObject _prefabFish;
    public List<GameObject> spawnPoint = new List<GameObject>();
    
    // PRIVATE ATTRIBUTE
    private List<GameObject> list_fish = new List<GameObject>();

    private List<Fish> listValueFish = new List<Fish>();
    private List<int> listID = new List<int>();

    public List<Sprite> listSprite = new List<Sprite>();

    private int nbrFish;

    public void initFishManager(int nbrFish, List<Sprite> listsprite)
    {
        for (int i = 0; i < listsprite.Count; i++)
        {
            listSprite.Add(listsprite[i]);
        }
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
        Debug.Log("" + id);
        GameObject fishTMP = Instantiate(_prefabFish);
        Fish fish = fishTMP.GetComponent<Fish>();
        fish.setImg(listSprite[id]);
        fishTMP.transform.position = spawnPoint[id].transform.position;
        Debug.Log(spawnPoint[id].name);

        list_fish.Add(fishTMP);
    }
}
