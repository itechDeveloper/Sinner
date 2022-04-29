using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerPetFollow : MonoBehaviour
{
    private Transform followingPoint;
    public float speed;

    public int damage;
    Collider2D[] enemies;
    public float viewRadius;
    public LayerMask whatIsEnemies;
    Transform target;
    public float attackRange;

    public GameObject explosionEffect;

    public float coolDown;
    void Start()
    {
        followingPoint = GameObject.FindGameObjectWithTag("petPos").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        Flip();
        coolDown -= Time.deltaTime;
        if (coolDown <= 0)
        {
            FindTarget();
        }
    }

    void Follow()
    {
        if (Vector2.Distance(transform.position, followingPoint.position) > .2f && target == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, followingPoint.position, speed);
        }else if( target != null) { 
            if (Vector2.Distance(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + .5f)) > .2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + .5f), speed);
            }else if (Vector2.Distance(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + .5f)) < .2f)
            {
                AttackerPetManager.isDead = true;
                Explode();
            }           
        }
    }

    void Flip()
    {
        if (target == null)
        {
            if (followingPoint.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (target.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
       
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
        }  
        Destroy(gameObject);    
    }

    void FindTarget()
    {
        enemies = Physics2D.OverlapCircleAll(transform.position, viewRadius, whatIsEnemies);
        target = null;
        for (int i = 0; i < enemies.Length; i++)
        {

            if (target == null)
            {
                target = enemies[i].transform;
            }
            else if (Vector2.Distance(target.transform.position, transform.position) > Vector2.Distance(transform.position, enemies[i].transform.position))
            {
                target = enemies[i].transform;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
