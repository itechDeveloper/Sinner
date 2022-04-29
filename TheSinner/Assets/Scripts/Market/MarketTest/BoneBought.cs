using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBought : MonoBehaviour
{
    private InventoryTest inventoryTest;
    public GameObject itemButton;
    public GameObject bone;
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
        for (int i = 0; i < inventoryTest.companionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                Destroy(bone);
                Instantiate(itemButton, inventoryTest.companionslots[i].transform, false);
                inventoryTest.cisFull[i] = true;
                break;
            }
        }
    }


}
