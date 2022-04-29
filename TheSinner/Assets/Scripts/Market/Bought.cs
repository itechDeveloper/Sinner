using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bought : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public CoinScript paralar;
    public void buyg()
    {
        paralar.gold -= 50;
    }

    public void buyb()
    {
        paralar.bone -= 50;
    }

    public void dontbuy()
    {
        item1.gameObject.SetActive(false);
        item2.gameObject.SetActive(false);
        item3.gameObject.SetActive(false);
    }
}
