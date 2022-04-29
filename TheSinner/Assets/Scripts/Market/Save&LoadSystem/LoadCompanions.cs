using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCompanions : MonoBehaviour
{
    public int i;
    public SaveSystem saveSystem;
    private UsedItemsTest usedItem;

    void Start()
    {
        usedItem = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
        CheckActiveItems();
    }

    public void CheckActiveItems()
    {
        if (PlayerPrefs.GetInt(("cinventoryUsedTest" + i)) == 1)
        {
            Instantiate(saveSystem.sUsedSlotCompanions[PlayerPrefs.GetInt("cslotUsedTestItem" + i)], usedItem.csslots[i].transform, false);
        }
    }
}
