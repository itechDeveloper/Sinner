using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBought : MonoBehaviour
{
    private InventoryTest inventoryTest;
    public GameObject itemButton;
    public GameObject stone;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUp()
    {
        for (int i = 0; i < inventoryTest.slots.Length; i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                Destroy(stone);
                Instantiate(itemButton, inventoryTest.slots[i].transform, false);
                inventoryTest.isFull[i] = true;
                break;
            }
        }
    }


}
