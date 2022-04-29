using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTestCompanion : MonoBehaviour
{
    public GameObject companion;
    private UsedItemsTest useditemtest;
    private InventoryTest inventoryTest;

    public string name;

    public int whichCompanion;

    public UsedSlotTest usedSlotTestCompanion;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        useditemtest = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    public void SpawnDroppedItem()
    {
        Debug.Log("dropped");
    }

    public void RemoveItem()
    {
        Debug.Log("removed");
        for (int i = 0; i < inventoryTest.companionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    PlayerPrefs.SetInt(name, 0);
                    Instantiate(companion, inventoryTest.companionslots[i].transform, false);
                    Destroy(useditemtest.csslots[transform.parent.GetComponent<UsedComponionSlotTest>().i].transform.GetChild(0).gameObject);
                }

                break;
            }
        }
    }
}
