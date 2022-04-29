using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elfAttack : MonoBehaviour
{
    private Animator animator;
    private Patrol patrol;

    public float attackRangeX;
    public float attackRangeY;
    public float attackRange;
    public float meleeRange;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;
    public GameObject arrow;

    Collider2D playerToDamage;
    Collider2D playerToMeleeDamage;
    Collider2D playerToMeleeReady;
    int attackCounter;
    bool canFlip;
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
        canFlip = true;
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        playerToMeleeDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
        playerToMeleeReady = Physics2D.OverlapCircle(attackPos.position, meleeRange, whatIsEnemies);
        patrol.canPatrol = false;
        patrol.patrolMovement = false;
        AttackPrep();
    }

    void AttackPrep()
    {
        if (playerToDamage != null)
        {
            if (playerToDamage.transform.position.x < transform.position.x && canFlip)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (playerToDamage.transform.position.x > transform.position.x && canFlip)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            patrol.canPatrol = false;
            patrol.patrolMovement = false;

            if (takeDamage.hit)
            {
                attackCounter = 0;
                animator.SetBool("meleeAttack", false);
                readyAttackTime = startReadyAttackTime;
                canFlip = true;
            }

            if (readyAttackTime <= 0)
            {
                if (playerToMeleeReady == null)
                {
                    readyAttackTime = startReadyAttackTime;
                    animator.SetBool("attacking", true);
                    attacking = true;
                    attackCounter = 0;
                }
                else
                {
                    animator.SetBool("meleeAttack", true);
                    canFlip = false;
                }
            }
            else
            {
                attackCounter = 0;
                animator.SetBool("meleeAttack", false);
                readyAttackTime -= Time.deltaTime;
                canFlip = true;
            }
        }
        else if (!attacking)
        {
            patrol.canPatrol = true;
            patrol.patrolMovement = true;
        }
    }

    public void AttackMelee()
    {
        if (playerToMeleeDamage != null)
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
                }
                else if (playerToDamage.transform.position.x < transform.position.x)
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

    public void AttackMeleeEnd()
    {
        if (attackCounter < 3)
        {
            attackCounter++;           
        }
        else
        {
            animator.SetBool("meleeAttack", false);
            readyAttackTime = startReadyAttackTime;
            attackCounter = 0;
            canFlip = true;
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
        patrol.canPatrol = true;
        patrol.patrolMovement = true;
        attacking = false;
        readyAttackTime = startReadyAttackTime;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(attackPos.position, meleeRange);
    }
}
