using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDamage : MonoBehaviour
{
    public Transform attackPos1;
    public Transform attackPos2;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public static int damage;

    public void Attack1()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos1.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
        }
    }

    public void Attack2()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos2.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos1.position, attackRange);
        Gizmos.DrawWireSphere(attackPos1.position, attackRange);
    }
}
