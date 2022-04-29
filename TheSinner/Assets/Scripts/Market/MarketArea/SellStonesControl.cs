using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellStonesControl : MonoBehaviour
{
    public int i;
    public SaveSystem saveSystem;
    int randomNum;

    void Start()
    {
        if(PlayerPrefs.GetInt("refreshMarket") == 1)
        {
            RefreshStones();
        }
        else
        {
            if (PlayerPrefs.GetInt("CanSellStone" + i) == 0)
            {
                Instantiate(saveSystem.marketStones[PlayerPrefs.GetInt("SellStone" + i)], transform.position, Quaternion.identity, transform);
            }
        }
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            PlayerPrefs.SetInt("CanSellStone" + i, 1);
        }
    }

    public void RefreshStones()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        randomNum = Random.Range(0, 8);
        Instantiate(saveSystem.marketStones[randomNum], transform.position, Quaternion.identity, transform);
        PlayerPrefs.SetInt("SellStone" + i, randomNum);
        PlayerPrefs.SetInt("CanSellStone" + i, 0);
    }
}
