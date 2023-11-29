using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour, IDataPersistence
{

    private string lastSceneName = "";

    public void LoadData(GameData data)
    {
        this.lastSceneName = data.lastSceneName;
    }

    public void SaveData(ref GameData data)
    {
        
    }

    public void MoveToScene()
    {
        if (lastSceneName == "")
        {
            lastSceneName = "SeaLevel";
        }
        Debug.Log("Moving to : " + lastSceneName);
        SceneManager.LoadScene(lastSceneName);
    }
}
