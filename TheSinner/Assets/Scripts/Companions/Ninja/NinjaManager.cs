using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaManager : MonoBehaviour
{
    public GameObject spawningEffect;
    public GameObject ninja;
    public GameObject player;

    public float coolDown;
    float coolDownTimer;
    Vector2 whereToSpawn;

    Collider2D[] enemies;
    Collider2D[] miniBoss;
    Collider2D[] boss;
    public float viewRadiusX;
    public float viewRadiusY;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsMiniBoss;
    public LayerMask whatIsBoss;
    internal static Transform targetEnemy;

    internal static bool facingRight;
    bool playerLeft;

    bool assasinated;
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
            assasinated = false;
            FindSpawningPlace();
            if (targetEnemy != null)
            {
                Vector2 whereToSpawnEffect = new Vector2(whereToSpawn.x - .4f, whereToSpawn.y + 1.4f);
                Instantiate(spawningEffect, whereToSpawnEffect, Quaternion.identity);
                Instantiate(ninja, whereToSpawn, Quaternion.identity);
                coolDownTimer = coolDown;
            }
        }
    }


    void FindSpawningPlace()
    {
        FindTargetEnemy();
        if (targetEnemy != null)
        {
            if (player.transform.position.x - targetEnemy.transform.position.x < 0)
            {
                playerLeft = true;
            }

            if (playerLeft)
            {

                whereToSpawn = new Vector2(targetEnemy.transform.position.x + .5f, targetEnemy.transform.position.y);
                facingRight = false;
            }
            else
            {

                whereToSpawn = new Vector2(targetEnemy.transform.position.x - .5f, targetEnemy.transform.position.y);
                facingRight = true;

            }
        }
    }

    void FindTargetEnemy()
    {
        enemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(viewRadiusX, viewRadiusY), 0, whatIsEnemies);
        miniBoss = Physics2D.OverlapBoxAll(transform.position, new Vector2(viewRadiusX, viewRadiusY), 0, whatIsMiniBoss);
        boss = Physics2D.OverlapBoxAll(transform.position, new Vector2(viewRadiusX, viewRadiusY), 0, whatIsBoss);
        targetEnemy = null;

        if (!assasinated)
        {
            for (int i = 0; i < miniBoss.Length; i++)
            {
                if (miniBoss[i].GetComponent<TakeDamage>().currentHealth < (enemies[i].GetComponent<TakeDamage>().health / 4))
                {
                    assasinated = true;
                    targetEnemy = miniBoss[i].transform;
                    break;
                }
                else
                {
                    targetEnemy = null;
                }
            }
        }

        if (!assasinated)
        {
            for (int i = 0; i < boss.Length - 1; i++)
            {
                if (boss[i].GetComponent<TakeDamage>().currentHealth < (enemies[i].GetComponent<TakeDamage>().health / 8))
                {
                    assasinated = true;
                    targetEnemy = boss[i].transform;
                    break;
                }
                else
                {
                    targetEnemy = null;
                }
            }
        }

        if (!assasinated)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<TakeDamage>().currentHealth < (enemies[i].GetComponent<TakeDamage>().health / 2))
                {
                    assasinated = true;
                    targetEnemy = enemies[i].transform;
                    break;
                }
                else
                {
                    targetEnemy = null;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(player.transform.position, new Vector2(viewRadiusX, viewRadiusY));
    }
}
