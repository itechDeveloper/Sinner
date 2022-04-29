using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHoodController : MonoBehaviour
{
    private Animator animator;

    public float attackRangeX;
    public float attackRangeY;
    public float chaseRangeX;
    public float chaseRangeY;
    public Transform chasingPoint;
    public float speed;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;

    Collider2D playerToDamage;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public int damage;

    bool canChase;
    Collider2D playerToChase;

    private TakeDamage takeDamage;

    private SpriteRenderer sprite;
    float dazeSpeed;

    bool attackDash;

    bool turnToMainPoint;
    float firstAttacTiming;
    Vector2 target;
    bool targetDedected;
    public GameObject redHoodDead;

    public GameObject arrow;

    public GameObject priceStone;
    void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        readyAttackTime = startReadyAttackTime;
        canChase = true;
        sprite = GetComponent<SpriteRenderer>();
        firstAttacTiming = 5f;
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        playerToChase = Physics2D.OverlapBox(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector2(chaseRangeX, chaseRangeY), 0, whatIsEnemies);
        MeleeAttackPrep();
        Daze();
        Chasing();
        Death();
    }

    void MeleeAttackPrep()
    {
        if (playerToDamage != null)
        {
            canChase = false;

            if (takeDamage.hitCounter < 3 && takeDamage.hit)
            {
                readyAttackTime = startReadyAttackTime;
            }

            if (readyAttackTime <= 0 && takeDamage.currentHealth > takeDamage.health/2)
            {
                readyAttackTime = startReadyAttackTime;
                animator.SetBool("heavyAttacking", true);
                attacking = true;
            }else if (readyAttackTime <= 0 && takeDamage.currentHealth < takeDamage.health / 2)
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
        else if (!attacking)
        {
            canChase = true;
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

    public void StartRangedAttack()
    {
        Instantiate(arrow, attackPos.position, Quaternion.identity);
    }


    public void AttackEnd()
    {
        animator.SetBool("rangedAttackBool", false);
        animator.SetBool("attacking", false);
        animator.SetBool("heavyAttacking", false);
        attacking = false;
        readyAttackTime = startReadyAttackTime;
        canChase = true;
    }

    void Chasing()
    {
        if (playerToChase != null && playerToDamage == null && firstAttacTiming > 0)
        {
            firstAttacTiming -= Time.deltaTime;
        }
        else if (playerToChase != null && playerToDamage != null && firstAttacTiming > 0)
        {
            firstAttacTiming = 5f;
        }
        else if (playerToChase == null)
        {
            firstAttacTiming = 5f;
        }

        if (firstAttacTiming <= 0)
        {
            if (!targetDedected)
            {
                if (playerToChase.transform.position.x > transform.position.x)
                {
                    target = new Vector2(playerToChase.transform.position.x + 1f, transform.position.y);
                }
                else if (playerToChase.transform.position.x < transform.position.x)
                {
                    target = new Vector2(playerToChase.transform.position.x - 1f, transform.position.y);
                }

                targetDedected = true;
            }
            attacking = true;
            canChase = false;
            animator.SetTrigger("rangedAttack");
            animator.SetBool("rangedAttackBool", true);
            firstAttacTiming = 5f;    
        }
        else if (playerToChase != null && !attacking)
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

        if (playerToChase != null && canChase && firstAttacTiming > 0)
        {
            animator.SetBool("walking", true);
            if (playerToChase.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (playerToChase.transform.position.x > transform.position.x)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
            sprite.color = new Color(.5f, .5f, .5f, 1);
            if (takeDamage.dazedTime > 0 && !attacking)
            {
                dazeSpeed = 1f;
                takeDamage.dazedTime -= Time.deltaTime;
                transform.Translate(new Vector2(-dazeSpeed, 0) * Time.deltaTime);
            }
            else
            {
                takeDamage.dazed = false;
            }
        }
    }

    public void EndDaze()
    {
        animator.SetBool("getHitBool", false);
        sprite.color = new Color(1, 1, 1, 1);
        takeDamage.dazed = false;
    }

    void Death()
    {
        if (takeDamage.dead)
        {
            redHoodDead.SetActive(true);
            Instantiate(redHoodDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }

            DeathControl.redHoodDead = true;
            Instantiate(priceStone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 0));
        Gizmos.DrawWireCube(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector3(chaseRangeX, chaseRangeY, 0));
    }
}
