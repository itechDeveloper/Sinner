using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponionSlotTest : MonoBehaviour
{
    private InventoryTest inventory;
    public int i;

    private InventoryTest inventoryTest;

    public SaveSystem saveSystem;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        
        if (PlayerPrefs.GetInt("ResetGame") == 1)
        {
            ResetInventory();
        }
        else
        {
            CheckItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.cisFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            PlayerPrefs.SetInt("cinventoryTest" + i, 0);
            inventory.cisFull[i] = false;
        }
    }

    public void CheckItem()
    {
        if (PlayerPrefs.GetInt("cinventoryTest" + i) == 1)
        {
            inventory.cisFull[i] = true;
            Instantiate(saveSystem.slotCompanions[PlayerPrefs.GetInt("cslotTestItem" + i)], inventoryTest.companionslots[i].transform, false);
        }
    }

    public void ResetInventory()
    {
        if (PlayerPrefs.GetInt("cinventoryTest" + i) == 1)
        {
            PlayerPrefs.SetInt("cinventoryTest" + i, 0);
        }
    }
}
