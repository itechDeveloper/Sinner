using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeAttack : MonoBehaviour
{
    private Animator animator;
    private Patrol patrol;

    public float attackRangeX;
    public float attackRangeY;
    public float chaseRangeX;
    public float chaseRangeY;
    public Transform chasingPoint;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;

    Collider2D playerToDamage;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public int damage;

    internal bool canChase;
    Collider2D playerToChase;
    public LayerMask whatIsToChase;

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
        playerToChase = Physics2D.OverlapBox(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector2(chaseRangeX, chaseRangeY), 0, whatIsToChase);
        MeleeAttackPrep();
        Chasing();
    }

    void MeleeAttackPrep()
    {
        if (playerToDamage != null)
        {
            patrol.canPatrol = false;
            patrol.patrolMovement = false;
            canChase = false;

            if (takeDamage.hitCounter < 3 && takeDamage.hit)
            {
                readyAttackTime = startReadyAttackTime;
            }

            if (readyAttackTime <= 0)
            {
                readyAttackTime = startReadyAttackTime;
                animator.SetBool("attacking", true);
                attacking = true;
            }
            else
            {
                readyAttackTime -= Time.deltaTime;
            }
        }
    }

    public void Attack()
    {
        if (playerToDamage != null)
        {
            if (!PlayerMovement.blocking)
            {
                if (playerToDamage.GetComponent<PlayerMovement>() != null)
                {
                    playerToDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
                }
                else
                {
                    playerToDamage.GetComponent<TakeDamage>().GetDamage(damage);
                }
                
                if (playerToDamage.transform.position.x > transform.position.x)
                {
                    PlayerMovement.dazeRight = true;
                }else if (playerToDamage.transform.position.x < transform.position.x)
                {
                    PlayerMovement.dazeRight = false;
                }
            }
            else
            {
                if (PlayerMovement.facingRight && playerToDamage.transform.position.x > transform.position.x)
                {
                    playerToDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
                    PlayerMovement.dazeRight = true;
                }
                else if (!PlayerMovement.facingRight && playerToDamage.transform.position.x < transform.position.x)
                {
                    playerToDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
                    PlayerMovement.dazeRight = false;
                }
            }
        }
    }

    public void AttackEnd()
    {
        animator.SetBool("attacking", false);
        attacking = false;
        readyAttackTime = startReadyAttackTime;
    }

    void Chasing()
    {
        if (playerToChase != null && !attacking)
        {
            if (playerToChase.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (playerToChase.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (playerToDamage == null)
            {
                canChase = true;
            }
        }

        if (!patrol.groundInfo.collider && playerToChase != null && canChase)
        {
            transform.Translate(Vector2.zero);
            patrol.patrolMovement = false;
        }
        else if (playerToChase != null && canChase && patrol.groundInfo.collider)
        {
            patrol.canPatrol = false;
            if (playerToChase.transform.position.x < transform.position.x && !attacking)
            {
                transform.Translate(Vector2.right * patrol.speed * Time.deltaTime);
            }
            else if (playerToChase.transform.position.x > transform.position.x && !attacking)
            {
                transform.Translate(Vector2.right * patrol.speed * Time.deltaTime);
            }
        }
        else if (playerToChase == null)
        {
            patrol.canPatrol = true;
            patrol.patrolMovement = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 0));
        Gizmos.DrawWireCube(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector3(chaseRangeX, chaseRangeY, 0));
    }
}
