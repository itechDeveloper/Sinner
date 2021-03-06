using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneItem : MonoBehaviour
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

        for (int i = 0; i < usedItems.cslots.Length; i++)
        {
            if (usedItems.isFull[i] == false)
            {
                Instantiate(itemButton, usedItems.cslots[i].transform, false);
                Destroy(gameObject);
                usedItems.isFull[i] = true;
                break;
            }
        }

    }
}