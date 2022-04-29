using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpellMovement : MonoBehaviour
{
    public float speed;
    public GameObject destroyAnimation;
    public int damage;
    internal Vector2 target;
    Vector3 direction;
    bool reachedToTarget;
    public GameObject explosionEffect;
    public float attackRange;
    public LayerMask whatIsEnemy;

    public float angleToMines;

    public float directionNumberX;
    public float directionNumberY;

    private void Start()
    {
        target = new Vector2(transform.position.x + directionNumberX, transform.position.y + directionNumberY);
        direction = -(new Vector2(transform.position.x, transform.position.y) - target);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angleToMines;
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
        if (!reachedToTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), target) < 0.2f)
            {
                Explode();
            }
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
