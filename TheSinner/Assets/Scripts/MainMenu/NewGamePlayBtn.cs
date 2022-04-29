using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGamePlayBtn : MonoBehaviour
{
    public GameObject aciklama;
    
    public GameObject options;

    public void play()
    {
        
        if(PlayerPrefs.GetInt("NewGame") == 1)
        {
            
            aciklama.SetActive(true);
        }
        else
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target1")
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("ResetGame", 1);
            options.gameObject.SetActive(true);
        }
    }
}
