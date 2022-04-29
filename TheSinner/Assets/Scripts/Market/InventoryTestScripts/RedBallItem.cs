using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallItem : MonoBehaviour
{
    private UsedItemsTest usedItems;
    public GameObject itemButton;

    void Start()
    {
        usedItems = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    public void Use()
    {
        Debug.Log("used");

        for (int i = 0; i < usedItems.slots.Length; i++)
        {
            if (usedItems.isFull[i] == false)
            {
                Instantiate(itemButton, usedItems.slots[i].transform, false);
                Destroy(gameObject);
                usedItems.isFull[i] = true;
                break;
            }
        }

    }
}