using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayBtn : MonoBehaviour
{
    
    public GameObject options;

    private void Start()
    {
        if (PlayerPrefs.GetInt("NewGame") != 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void play()
    { 
        Time.timeScale = 1f;
        options.gameObject.SetActive(true);
        SceneManager.LoadScene(PlayerPrefs.GetInt("ActiveScene"));
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target1" )
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("ResetGame", 1);
            options.gameObject.SetActive(true);
        }
    }


}
