using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevamEt_btn : MonoBehaviour
{
    public GameObject options;
    public void devamet()
    {
        PlayerPrefs.SetInt("NewGame", 1);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ResetGame", 1);
        PlayerPrefs.SetInt("refreshMarket", 1);
        options.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt(("ActiveScene"), 20);
        SceneManager.LoadScene(20);
    }
}
