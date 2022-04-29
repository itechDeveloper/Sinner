using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearManager : MonoBehaviour
{
    public float speed;
    public GameObject destroyAnimation;
    public int damage;
    internal Vector2 target;
    private GameObject player;
    Vector3 direction;
    public GameObject explosionEffect;
    public float attackRange;
    public LayerMask whatIsEnemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = new Vector2(player.transform.position.x, player.transform.position.y + .5f);
        direction = transform.position - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        direction.Normalize();
        transform.position = Vector2.MoveTowards(transform.position, target, speed);
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), target) < 0.2f)
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
        if (collision.gameObject.tag == "Player")
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

            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "shield")
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
