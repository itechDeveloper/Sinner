using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingComp : MonoBehaviour
{
    public int cost;
    public GameObject notenoughmoney;
    public ComponionPickUp componionPickUp;
    public GameObject infos;

    public void Buy()
    {
        if (GoldScore.gold >= cost)
        {
            infos.gameObject.SetActive(false);
            notenoughmoney.gameObject.SetActive(false);
            transform.parent.parent.parent.GetComponent<ComponionPickUp>().enabled = true;
            componionPickUp.canPickUp = true;
            GoldScore.gold -= cost;
        }

        else
        {
            notenoughmoney.gameObject.SetActive(true);
        }
    }
}
