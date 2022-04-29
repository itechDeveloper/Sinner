using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    int randomNum;
    float spawnCd;
    public float startSpawnCd;
    public bool mageSpawner;
    public GameObject spawnEffect;

    private void Start()
    {
        if (mageSpawner)
        {
            spawnCd = startSpawnCd;
        }
    }

    void Update()
    {
        if (spawnCd <= 0)
        {
            randomNum = Random.Range(0, enemies.Length);
            if (mageSpawner)
            {
                Instantiate(spawnEffect, new Vector2(transform.position.x -.2f, transform.position.y + 1f), Quaternion.identity);
            }
            Instantiate(enemies[randomNum], transform.position, Quaternion.identity);
            spawnCd = startSpawnCd;
        }
        else
        {
            spawnCd -= Time.deltaTime;
        }
    }
}
