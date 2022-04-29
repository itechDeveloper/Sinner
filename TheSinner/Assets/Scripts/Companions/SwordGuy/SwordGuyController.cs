using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuyController : MonoBehaviour
{
    public GameObject destroyingEffect;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    public int attackCounter;
    private Animator animator;
    bool attacked;
    void Start()
    {
        animator = GetComponent<Animator>();
        attackCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimation();
        Facing();
    }

    void AttackAnimation()
    {
        if (!attacked)
        {
            animator.SetBool("attack", true);
        }
    }

    public void Attack()
    {
        if (attackCounter < 6)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(1.7f, .9f), 0 , whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
            }
            attacked = true;
            attackCounter++;
        }
    }

    public void Destroy()
    {
        if (attackCounter >= 5)
        {
            Instantiate(destroyingEffect, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
            Destroy(gameObject);
        }   
    }

    void Facing()
    {
        if (SwordGuyManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(1.7f, .9f, 0));
    }
}
