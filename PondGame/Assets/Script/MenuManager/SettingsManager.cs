using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public void MoveToScene()
    {
        GameManager.instance.changingScene("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToLastScene()
    {
        SceneManager.LoadScene(GameManager.instance.lastSceneName);
        GameManager.instance.changingScene("SeaLevel");
    }
}
