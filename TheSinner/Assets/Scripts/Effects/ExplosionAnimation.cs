using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemy;
    Collider2D [] whatIsToDamage;
    public int damage;

    void Update()
    {
        whatIsToDamage = Physics2D.OverlapBoxAll(transform.position, new Vector2(attackRangeX,attackRangeY),0, whatIsEnemy);
    }

    public void Attack()
    {
        if (whatIsToDamage != null)
        {
            for(int i = 0; i < whatIsToDamage.Length; i++)
            {
                whatIsToDamage[i].GetComponent<PlayerMovement>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(attackRangeX,attackRangeY));
    }
}
