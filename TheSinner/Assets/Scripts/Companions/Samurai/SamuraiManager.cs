using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiManager : MonoBehaviour
{
    public GameObject spawningEffect;
    public GameObject samurai;
    public GameObject player;

    public float coolDown;
    float coolDownTimer;
    Vector2 whereToSpawn;

    internal static Collider2D[] enemies;
    public float viewRadiusX;
    public float viewRadiusY;
    public LayerMask whatIsEnemies;

    internal static bool facingRight;
    bool playerLeft;

    private void Start()
    {
        coolDownTimer = coolDown;
    }
    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        Spawn();
    }

    void Spawn()
    {
        if (coolDownTimer <= 0)
        {
            FindSpawningPlace();
            if (enemies.Length > 1)
            {
                if (enemies[0] != null)
                {
                    Vector2 whereToSpawnEffect = new Vector2(whereToSpawn.x - .4f, whereToSpawn.y + 1.4f);
                    Instantiate(spawningEffect, whereToSpawnEffect, Quaternion.identity);
                    Instantiate(samurai, whereToSpawn, Quaternion.identity);
                    coolDownTimer = coolDown;
                }
            }
        }
    }


    void FindSpawningPlace()
    {
        FindTargetEnemy();
        if (enemies.Length > 1)
        {
            if (enemies[0] != null)
            {
                if (player.transform.position.x - enemies[0].transform.position.x < 0)
                {
                    playerLeft = true;
                }

                if (playerLeft)
                {
                    whereToSpawn = new Vector2(enemies[0].transform.position.x + .5f, enemies[0].transform.position.y);
                    facingRight = false;
                }
                else
                {
                    whereToSpawn = new Vector2(enemies[0].transform.position.x - .5f, enemies[0].transform.position.y);
                    facingRight = true;
                }
            }
        }
    }

    void FindTargetEnemy()
    {
        enemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(viewRadiusX,viewRadiusY), 0, whatIsEnemies);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(player.transform.position, new Vector2(viewRadiusX, viewRadiusY));
    }
}
