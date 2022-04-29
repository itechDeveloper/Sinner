using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMovement : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector2 targetPos;
    public GameObject explosionEffect;
    public float attackRange;
    public LayerMask whatIsEnemy;
    Collider2D[] enemiesToDamage;

    private void Start()
    {
        if (ArcherManager.facingRight)
        {
            targetPos = new Vector3(transform.position.x + 10, transform.position.y, 0);
        }
        else
        {
            targetPos = new Vector3(transform.position.x - 10, transform.position.y, 0);
        }
        
    }

    void Update()
    {
        enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemy);
        Move();
    }

    void Move()
    {
        if (ArcherManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0,0,-135);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, targetPos) < .2f)
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity); 
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TakeDamage>().GetDamage(damage);
        }
    }
}
