using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlashLeft : MonoBehaviour
{
    float timer = 2f;
    public float speed;
    public GameObject explosionEffect;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<PlayerMovement>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.GetComponent<PlayerMovement>().hittable)
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                PlayerMovement.dazeRight = true;
            }
            else if (collision.gameObject.transform.position.x < transform.position.x)
            {
                PlayerMovement.dazeRight = false;
            }

            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "shield")
        {
            Destroy(gameObject);
        }
    }
}
