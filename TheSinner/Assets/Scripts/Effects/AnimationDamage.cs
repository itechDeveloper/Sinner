using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDamage : MonoBehaviour
{
    public float attackRange;
    public LayerMask whatIsEnemy;
    Collider2D[] whatIsToDamage;
    public int damage;
    public Transform attackPos;

    void Update()
    {
        whatIsToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
    }

    public void Attack()
    {
        if (whatIsToDamage != null)
        {
            for (int i = 0; i < whatIsToDamage.Length; i++)
            {
                whatIsToDamage[i].GetComponent<PlayerMovement>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
