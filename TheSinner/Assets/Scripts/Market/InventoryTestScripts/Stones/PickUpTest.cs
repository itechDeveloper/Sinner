using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTest : MonoBehaviour
{
    private InventoryTest inventoryTest;
    public GameObject itemButton;

    public int whichStone;

    public bool canPickUp;

    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    void PickUp()
    {
        for (int i = 0; i < inventoryTest.slots.Length ;i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                Destroy(gameObject);
                Instantiate(itemButton, inventoryTest.slots[i].transform, false);
                inventoryTest.isFull[i] = true;
                PlayerPrefs.SetInt("inventoryTest" + i, 1);
                PlayerPrefs.SetInt("slotTestItem" + i, whichStone);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canPickUp)
        {
            PickUp();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canPickUp)
        {
            PickUp();
        }
    }
}
