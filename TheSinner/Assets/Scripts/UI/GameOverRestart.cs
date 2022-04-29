using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverRestart : MonoBehaviour
{
    public void restart()
    {
        PlayerPrefs.SetInt("refreshMarket", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("ActiveScene"));
    }

    public void anamenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
