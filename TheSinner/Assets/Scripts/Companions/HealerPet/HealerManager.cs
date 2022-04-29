using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerManager : MonoBehaviour
{
    public GameObject healerPet;
    public GameObject player;

    Vector2 whereToSpawn;



    void Update()
    {

        if (player.GetComponent<PlayerMovement>().currentHealth < player.GetComponent<PlayerMovement>().maxHealth)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (PlayerMovement.canRegenHealth)
        {
            FindSpawningPlace();
            Instantiate(healerPet, whereToSpawn, Quaternion.identity);
            PlayerMovement.canRegenHealth = false;
        }
    }

    void FindSpawningPlace()
    {
        whereToSpawn = new Vector2(player.transform.position.x - .5f, player.transform.position.y + 1f);
    }
}
