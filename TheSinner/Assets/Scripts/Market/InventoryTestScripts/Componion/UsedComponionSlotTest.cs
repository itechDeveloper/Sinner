using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedComponionSlotTest : MonoBehaviour
{
    private InventoryTest inventoryTest;
    private UsedItemsTest usedItem;
    public int i; 

    public SaveSystem saveSystem;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        usedItem = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
        
        if (PlayerPrefs.GetInt("ResetGame") == 1)
        {
            ResetInventory();
        }
        else
        {
            CheckItem();
        }
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            usedItem.cisFull[i] = false;
        }
    }

    public void RemoveItem()
    {
        for (int i = 0; i < inventoryTest.companionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpawnTestCompanion>().RemoveItem();

                    PlayerPrefs.SetInt("cinventoryUsedTest" + this.i, 0);

                    PlayerPrefs.SetInt("cinventoryTest" + i, 1);
                    PlayerPrefs.SetInt("cslotTestItem" + i, child.GetComponent<SpawnTestCompanion>().whichCompanion);

                    Destroy(child.gameObject);
                    inventoryTest.cisFull[i] = true;
                }
                break;
            }
        }
    }

    public void CheckItem()
    {
        if (PlayerPrefs.GetInt("cinventoryUsedTest" + i) == 1)
        {
            usedItem.cisFull[i] = true;
            Instantiate(saveSystem.usedSlotCompanions[PlayerPrefs.GetInt("cslotUsedTestItem" + i)], usedItem.cslots[i].transform, false);
        }
    }

    public void ResetInventory()
    {
        if (PlayerPrefs.GetInt("cinventoryUsedTest" + i) == 1)
        {
            PlayerPrefs.SetInt("cinventoryUsedTest" + i, 0);
        }
    }
}
