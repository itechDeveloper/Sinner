using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrafting : MonoBehaviour
{
    public GameObject item;
    private InventoryTest inventoryTest;

    public int whichStone;
    public GameObject nextStone;
    public int nextStoneInt;

    public QuitCrafting quitCrafting;

    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        quitCrafting = GameObject.FindGameObjectWithTag("BackButton").GetComponent<QuitCrafting>();
    }

    public void RemoveItem()
    {
        Instantiate(item, inventoryTest.slots[quitCrafting.slots[transform.parent.GetComponent<UsedCraft>().i]].transform, false);
        quitCrafting.slots[transform.parent.GetComponent<UsedCraft>().i] = -1;
    }
}
