using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScore : MonoBehaviour
{
    public static int gold;
    public Text goldview;
    public Text notenoughmoney;

    private void Start()
    {
        notenoughmoney.gameObject.SetActive(false);
        gold = PlayerPrefs.GetInt("Gold");        
    }

    public void Update()
    {
        goldview.text = "" + gold + "G";
    }

    public static void UpdateGold()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }
}
