using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellStone : MonoBehaviour
{
    public int cost;

    public void Sell()
    {
        PlayerPrefs.SetInt("inventoryTest" + transform.parent.parent.parent.GetComponent<SlotTest>().i, 0);
        GoldScore.gold += cost;
        Destroy(transform.parent.parent.gameObject);
    }
}
