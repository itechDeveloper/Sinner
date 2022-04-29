using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketGecis : MonoBehaviour
{
    public GameObject marketgecisbuton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            marketgecisbuton.gameObject.SetActive(true);
            PlayerPrefs.SetInt("ResetGame", 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            marketgecisbuton.gameObject.SetActive(false);
        }
    }
}
