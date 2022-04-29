using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGhostManager : MonoBehaviour
{
    private Animator animator;
    private GameObject player;

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

    Collider2D playerToChase;
    public LayerMask whatIsToChase;

    private TakeDamage takeDamage;

    float teleportCd;
    public float startTeleportCd;

    public GameObject skeletonGhostDead;
    void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        readyAttackTime = startReadyAttackTime;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        playerToChase = Physics2D.OverlapBox(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector2(chaseRangeX, chaseRangeY), 0, whatIsToChase);
        MeleeAttackPrep();
        Teleport();
        Facing();
        Death();
    }

    void Facing()
    {
        if (!attacking)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
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

    public void AttackEnd()
    {
        animator.SetBool("attacking", false);
        attacking = false;
        readyAttackTime = startReadyAttackTime;
    }

    void Teleport()
    {
        if(playerToChase != null && playerToDamage == null)
        {
            if (teleportCd <= 0)
            {
                animator.SetBool("teleportFrom", true);
                teleportCd = startTeleportCd;
            }
            else
            {
                teleportCd -= Time.deltaTime;
            }
        }
    }


    public void TeleportFrom()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.position = new Vector2(player.transform.position.x -.75f, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(player.transform.position.x + .75f, transform.position.y);
        }
        animator.SetBool("teleportTo", true);
        animator.SetBool("teleportFrom", false);
        readyAttackTime = 0f;
    }

    public void TeleportTo()
    {
        animator.SetBool("teleportTo", false);
    }

    void Death()
    {
        if (takeDamage.dead)
        {
            skeletonGhostDead.SetActive(true);
            Instantiate(skeletonGhostDead, new Vector2(transform.position.x, transform.position.y -.25f), Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }
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
