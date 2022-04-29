using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAndCloseCompanion : MonoBehaviour
{
    private UsedItemsTest usedItems;
    public GameObject companiontousedBtn;
    public GameObject companionButton;
    public GameObject info1;
    public GameObject companion;

    public string companionName;

    public int whichCompanion;
    void Start()
    {
        usedItems = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    public void Equip()
    {
        Close();

        for (int i = 0; i < usedItems.cslots.Length; i++)
        {
            if (usedItems.cisFull[i] == false)
            {
                PlayerPrefs.SetInt(companionName, 1);
                Instantiate(companionButton, usedItems.cslots[i].transform, false);
                Instantiate(companiontousedBtn, usedItems.csslots[i].transform, false);

                PlayerPrefs.SetInt("cinventoryUsedTest" + i, 1);
                PlayerPrefs.SetInt("cslotUsedTestItem" + i, whichCompanion);
                PlayerPrefs.SetInt("cinventoryTest" + transform.parent.parent.parent.GetComponent<ComponionSlotTest>().i, 0);

                Destroy(companion);

                usedItems.cisFull[i] = true;
                usedItems.csisFull[i] = true;
                break;
            }
        }
    }

    public void Close()
    {
        info1.gameObject.SetActive(false);
    }
}
