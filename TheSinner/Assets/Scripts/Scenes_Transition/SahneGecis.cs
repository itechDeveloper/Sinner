using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public GameObject transactionPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("ResetGame", 0);
            transactionPanel.SetActive(true);
            transactionPanel.GetComponent<Animator>().SetTrigger("sceneTransaction");
            PlayerPrefs.SetInt("refreshMarket", 1);
        }
    }    
}
