using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    private Animator animator;
    private Patrol patrol;

    public float attackRangeX;
    public float attackRangeY;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;
    public GameObject arrow;

    Collider2D playerToDamage;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public int damage;

    private TakeDamage takeDamage;

    void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        readyAttackTime = startReadyAttackTime;
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        AttackPrep();
    }

    void AttackPrep()
    {
        if (playerToDamage != null)
        {
            if (playerToDamage.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0,180,0);
            }
            if (playerToDamage.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            patrol.canPatrol = false;
            patrol.patrolMovement = false;

            if (takeDamage.hit)
            {
                readyAttackTime = startReadyAttackTime;
            }

            if (readyAttackTime <= 0)
            {
                readyAttackTime = startReadyAttackTime;
                if (playerToDamage.transform.position.y < transform.position.y - 1f)
                {
                    animator.SetBool("attacking2", true);
                }else if (playerToDamage.transform.position.y > transform.position.y + 1f)
                {
                    animator.SetBool("attacking1", true);
                }
                else
                {
                    animator.SetBool("attacking", true);
                }
                attacking = true;
            }
            else
            {
                readyAttackTime -= Time.deltaTime;
            }
        }
        else if (!attacking)
        {
            patrol.canPatrol = true;
            patrol.patrolMovement = true;
        }
    }

    public void Attack()
    {
        if (playerToDamage != null)
        {
            Instantiate(arrow, attackPos.position, Quaternion.identity);
        }
    }

    public void AttackEnd()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("attacking1", false);
        animator.SetBool("attacking2", false);
        patrol.canPatrol = true;
        patrol.patrolMovement = true;
        attacking = false;
        readyAttackTime = startReadyAttackTime;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}
