using UnityEngine;

public class UsedSlotTest : MonoBehaviour
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
            usedItem.isFull[i] = false;
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
                    child.GetComponent<SpawnTest>().RemoveItem();

                    PlayerPrefs.SetInt("inventoryUsedTest" + this.i, 0);

                    PlayerPrefs.SetInt("inventoryTest" + i, 1);
                    PlayerPrefs.SetInt("slotTestItem" + i, child.GetComponent<SpawnTest>().whichStone);

                    PlayerMovement.canCheckStats = true;

                    Destroy(child.gameObject);
                    inventoryTest.isFull[i] = true;
                }
                break;
            }
        }       
    }

    public void CheckItem()
    {
        if (PlayerPrefs.GetInt("inventoryUsedTest" + i) == 1)
        {
            usedItem.isFull[i] = true;
            Instantiate(saveSystem.usedSlotStones[PlayerPrefs.GetInt("slotUsedTestItem" + i)], usedItem.slots[i].transform, false);
        }
    }

    void ResetInventory()
    {
        if (PlayerPrefs.GetInt("inventoryUsedTest" + i) == 1)
        {
            PlayerPrefs.SetInt("inventoryUsedTest" + i, 0);
        }
    }
}
