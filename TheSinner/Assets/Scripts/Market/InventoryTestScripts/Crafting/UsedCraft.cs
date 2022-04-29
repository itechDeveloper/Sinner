using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedCraft : MonoBehaviour
{
    private InventoryTest inventoryTest;
    private CraftingSlots craftingItem;

    public int i;

    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        craftingItem = GameObject.FindGameObjectWithTag("Player").GetComponent<CraftingSlots>();
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            craftingItem.isFull[i] = false;
        }
    }

    public void RemoveItem()
    {
        for (int i = 0; i < inventoryTest.slots.Length; i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpawnCrafting>().RemoveItem();   
                    Destroy(child.gameObject);
                    inventoryTest.isFull[i] = true;
                }
                break;
            }
        }
    }
}
