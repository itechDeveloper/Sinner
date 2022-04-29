using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMobManager : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    bool canFace;

    public float attackRangeX;
    public float attackRangeY;
    public float speed;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;

    Collider2D playerToDamage;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public int damage;

    private TakeDamage takeDamage;

    private SpriteRenderer sprite;
    float dazeSpeed;

    float dashAttackTiming;
    bool canDash;
    Vector2 target;
    bool targetDedected;

    public Transform point1;
    public Transform point2;
    bool canDashToPoints;
    bool dashing;
    void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        readyAttackTime = startReadyAttackTime;
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        canFace = true;
        dashAttackTiming = 2f;
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        MeleeAttackPrep();
        Daze();
        Facing();
        if (!dashing)
        {
            FindTarget();
            DashAttack();
        }
        Dash();
    }

    void Facing()
    {
        if (attacking)
        {
            canFace = false;
        }
        else if(!dashing)
        {
            canFace = true;
        }

        if (canFace)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0,-180,0);
            }
        }
    }

    void MeleeAttackPrep()
    {
        if (playerToDamage != null)
        { 
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

    void Dash()
    {
        if (canDashToPoints)
        {
            canFace = false;
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                if (Vector2.Distance(point1.position, transform.position) < .2f)
                {
                    animator.SetBool("dash", false);
                    canFace = true;
                    canDashToPoints = false;
                    dashing = false;
                }
                else
                {
                    animator.SetBool("dash", true);
                    transform.position = Vector2.MoveTowards(transform.position, point1.position, speed * 10f * Time.deltaTime);
                    canDash = false;
                    dashing = true;
                }
            }
            else
            {
                if (Vector2.Distance(point2.position, transform.position) < .2f)
                {
                    animator.SetBool("dash", false);
                    canFace = true;
                    canDashToPoints = false;
                    dashing = false;

                }
                else
                {
                    animator.SetBool("dash", true);
                    transform.position = Vector2.MoveTowards(transform.position, point2.position, speed * 10f * Time.deltaTime);
                    canDash = false;
                    dashing = true;
                }
            }
        }
    }

    void FindTarget()
    {
        if (playerToDamage == null && !targetDedected)
        {
            if (dashAttackTiming <= 0f)
            {
                target = new Vector2(player.transform.position.x, transform.position.y);
                dashAttackTiming = 2f;
                canDash = true;
                targetDedected = true;
                
            }
            else if(!attacking)
            {
                dashAttackTiming -= Time.deltaTime;
            }
        }
    }

    void DashAttack()
    {
        if (targetDedected)
        {
            if (Vector2.Distance(transform.position, target) < .2f || playerToDamage != null && canDash)
            {
                animator.SetBool("dash", false);
                animator.SetBool("dashAttack", true);
                canDash = false;
            }
            else if (canDash)
            {
                attacking = true;
                animator.SetBool("dash", true);
                if(playerToDamage == null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target, speed * 10f * Time.deltaTime);
                }
                canFace = false;
            }
        }
    }

    public void EndDashAttack()
    {
        animator.SetBool("dashAttack", false);
        targetDedected = false;
        canDash = false;
        attacking = false;
    }

    public void AttackEnd()
    {
        canDashToPoints = true;
        animator.SetBool("attacking", false);
        attacking = false;
        readyAttackTime = startReadyAttackTime;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 0));
    }
}
