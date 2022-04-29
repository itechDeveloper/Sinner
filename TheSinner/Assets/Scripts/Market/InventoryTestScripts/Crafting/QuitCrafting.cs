using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCrafting : MonoBehaviour
{
    public int[] slots = new int [3];
    private InventoryTest inventoryTest;
    public GameObject[] slotsReal;
    private GoldScore gold;

    void Start()
    {
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<GoldScore>();
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        for (int i = 0; i < 3; i++)
        {
            slots[i] = -1;
        }
    }

    public void Quit()
    {
        gold.notenoughmoney.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            if (slots[i] != -1)
            {
                Instantiate(slotsReal[i].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().item, inventoryTest.slots[slots[i]].transform, false);
                Destroy(slotsReal[i].gameObject.transform.GetChild(0).gameObject);
                slots[i] = -1;
            }
        }  
    }
}
