using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingAssassinAttack : MonoBehaviour
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

    bool canChase;
    Collider2D playerToChase;
    public LayerMask whatIsToChase;

    private TakeDamage takeDamage;
    public CapsuleCollider2D collider2D;
    public bool canRoll;
    bool rolling;

    bool canDash;
    Vector2 target;
    bool reachedToTarget;
    GameObject player;
    bool targetDedected;
    bool dashing;

    bool face;

    void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        readyAttackTime = startReadyAttackTime;
        canRoll = true;
        canDash = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        playerToChase = Physics2D.OverlapBox(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector2(chaseRangeX, chaseRangeY), 0, whatIsToChase);
        if (!targetDedected && playerToChase != null)
        {
            target = new Vector2(player.transform.position.x, transform.position.y);
            targetDedected = true;
        }
        MeleeAttackPrep();
        Chasing();
        Roll();
        Dash();
    }
    void MeleeAttackPrep()
    {
        if (canChase && canDash && playerToChase != null)
        {
            animator.SetBool("dashing", true);
            animator.SetTrigger("dash");
            canDash = false;
            dashing = true;
        }
        
        if (takeDamage.hit && canRoll)
        {
            rolling = true;
            canRoll = false;
            animator.SetTrigger("roll");
            animator.SetBool("rolling", true);
            collider2D.enabled = false;
        }
        else if (playerToDamage != null)
        {
            patrol.canPatrol = false;
            patrol.patrolMovement = false;
            canChase = false;

            if (takeDamage.hitCounter < 3 && takeDamage.hit)
            {
                readyAttackTime = startReadyAttackTime;
            }

            if (readyAttackTime <= 0 && !rolling)
            {
                readyAttackTime = startReadyAttackTime;
                animator.SetBool("attacking1", true);
                attacking = true;
                canRoll = true;
            }
            else
            {
                readyAttackTime -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("walking", true);
            canChase = true;
        }
    }

    public void Roll()
    {
        if (rolling)
        {
            transform.Translate(Vector2.right * 2 * patrol.speed * Time.deltaTime);
        }
    }

    void Dash()
    {
        if (playerToChase != null && !face)
        {
            face = true;
            if (playerToChase.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (playerToChase.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (Vector2.Distance(transform.position, target) < .2f && !reachedToTarget && playerToChase != null)
        {
            reachedToTarget = true;
            animator.SetBool("dashing", false);
            canDash = false;
            animator.SetTrigger("attack2");
            animator.SetBool("attacking2", true);
            attacking = true;
            dashing = false;
        }
        else if(!reachedToTarget && playerToChase != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, patrol.speed * 10 * Time.deltaTime);
        }
    }

    public void Attack()
    {
        if (playerToDamage != null)
        {
            if (!PlayerMovement.blocking)
            {
                playerToDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
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

    public void AttackEnd()
    {
        animator.SetBool("attacking1", false);
        animator.SetBool("attacking2", false);
        attacking = false;
        readyAttackTime = startReadyAttackTime;
    }

    public void RollEnd()
    {
        animator.SetBool("rolling", false);
        if (playerToChase.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (playerToChase.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        animator.SetBool("attacking2", true);
        attacking = true;
        canRoll = false;
        collider2D.enabled = true;
        rolling = false;
    }

    void Chasing()
    {
        if (playerToChase != null && !attacking && !rolling && !dashing)
        {
            if (playerToChase.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (playerToChase.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
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
            animator.SetBool("walking", true);
            if (playerToChase.transform.position.x < transform.position.x && !attacking && !rolling)
            {
                transform.Translate(Vector2.right * patrol.speed * Time.deltaTime);
            }
            else if (playerToChase.transform.position.x > transform.position.x && !attacking && !rolling)
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
