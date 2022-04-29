using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
  
    
    public void menuac()
    {
        menu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void menukapat()
    {
        menu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void anamenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
