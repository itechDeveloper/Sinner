using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarketSahnesiYukleme : MonoBehaviour
{
    public string geldigimizsahne;
    
    public void marketegecis()
    {
        SceneManager.LoadScene("Market");
    }
}
