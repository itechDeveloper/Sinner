using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellCompanionControl : MonoBehaviour
{
    public SaveSystem saveSystem;
    int randomNum;

    void Start()
    {
        if (PlayerPrefs.GetInt("refreshMarket") == 1)
        {
            RefreshCompanion();           
        }
        else
        {
            Instantiate(saveSystem.marketCompanions[PlayerPrefs.GetInt("SellCompanion")], transform.position, Quaternion.identity, transform);
        }
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            PlayerPrefs.SetInt("CanSellCompanion", 1);
        }
    }

    public void RefreshCompanion()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        randomNum = Random.Range(0, 8);
        Instantiate(saveSystem.marketCompanions[randomNum], transform.position, Quaternion.identity, transform);
        PlayerPrefs.SetInt("SellCompanion", randomNum);
        PlayerPrefs.SetInt("refreshMarket", 0);
        PlayerPrefs.SetInt("CanSellCompanion", 0);
    }
}
