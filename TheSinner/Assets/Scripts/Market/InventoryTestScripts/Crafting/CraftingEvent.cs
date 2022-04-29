using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingEvent : MonoBehaviour
{
    public GameObject[] slots;
    private InventoryTest inventoryTest;
    int slot;
    private GoldScore gold;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<GoldScore>();
    }

    public void Craft()
    {
        if(slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().nextStone.transform.GetChild(0).transform.GetChild(1).GetComponent<EquipandCloseStone>().level == 2)
        {
            if(GoldScore.gold >= 50)
            {
                if (slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone ==
                slots[1].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone
                 && slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone ==
                 slots[2].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone)
                {
                    for (int i = 0; i < inventoryTest.slots.Length; i++)
                    {
                        if (inventoryTest.isFull[i] == false)
                        {
                            Instantiate(slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().nextStone,
                                inventoryTest.slots[i].transform, false);
                            slot = i;
                            inventoryTest.isFull[i] = true;
                            CheckStones();
                            Destroy(slots[0].gameObject.transform.GetChild(0).gameObject);
                            Destroy(slots[1].gameObject.transform.GetChild(0).gameObject);
                            Destroy(slots[2].gameObject.transform.GetChild(0).gameObject);
                            break;
                        }
                    }
                }
                GoldScore.gold -= 50;
                gold.notenoughmoney.gameObject.SetActive(false);
            }
            
            else
            {
                gold.notenoughmoney.gameObject.SetActive(true);
            }
        }
        else 
        {
            if (GoldScore.gold >= 100)
            {
                if (slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone ==
                slots[1].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone
                 && slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone ==
                 slots[2].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone)
                {
                    for (int i = 0; i < inventoryTest.slots.Length; i++)
                    {
                        if (inventoryTest.isFull[i] == false)
                        {
                            Instantiate(slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().nextStone,
                                inventoryTest.slots[i].transform, false);
                            slot = i;
                            inventoryTest.isFull[i] = true;
                            CheckStones();
                            Destroy(slots[0].gameObject.transform.GetChild(0).gameObject);
                            Destroy(slots[1].gameObject.transform.GetChild(0).gameObject);
                            Destroy(slots[2].gameObject.transform.GetChild(0).gameObject);
                            break;
                        }
                    }
                }
                GoldScore.gold -= 100;
                gold.notenoughmoney.gameObject.SetActive(false);
            }

            else
            {
                gold.notenoughmoney.gameObject.SetActive(true);
            }
        }

        
    }

    public void CheckStones()
    {
        PlayerPrefs.SetInt("inventoryTest" + slot, 1);
        PlayerPrefs.SetInt("slotTestItem" + slot, slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().nextStoneInt);

        for (int i = 0; i < inventoryTest.slots.Length; i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                PlayerPrefs.SetInt("inventoryTest" + i, 0);
            }    
        }
    }
}

