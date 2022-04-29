using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipandCloseStone : MonoBehaviour
{
    private UsedItemsTest usedItems;
    public GameObject itemtousedBtn;
    public GameObject itemButton;
    public GameObject info1;
    public GameObject item;

    public string stoneName;
    public int level;

    public int whichStone;
    void Start()
    {
        usedItems = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    public void Equip()
    {
        Close();

        for (int i = 0; i < usedItems.slots.Length; i++)
        {
            if (usedItems.isFull[i] == false)
            {
                PlayerPrefs.SetInt(stoneName + level, 1);
                Instantiate(itemButton, usedItems.slots[i].transform, false);
                Instantiate(itemtousedBtn, usedItems.sslots[i].transform, false);

                PlayerPrefs.SetInt("inventoryUsedTest" + i, 1);
                PlayerPrefs.SetInt("slotUsedTestItem" + i, whichStone);
                PlayerPrefs.SetInt("inventoryTest" + transform.parent.parent.parent.GetComponent<SlotTest>().i, 0);

                usedItems.isFull[i] = true;
                usedItems.sisFull[i] = true;

                PlayerMovement.canCheckStats = true;

                Destroy(item);
                break; 
            }    
        }
    }

    public void Close()
    {
        info1.gameObject.SetActive(false);
    }
}
