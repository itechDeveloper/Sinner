using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingBossController : MonoBehaviour
{
    private TakeDamage takeDamage;
    private SpriteRenderer sprite;
    public GameObject vikingDead;
    public Collider2D col2d;

    public float speed;
    public int damage;

    bool canChase;
    bool canFace;
    public float chaseCoolDown;
    float chaseCd;

    private Animator animator;
    private GameObject player;

    bool attacking;
    Collider2D playerToDamage;
    public Transform attackPos;
    public float attackRange;

    Collider2D playerToLeap;
    public Transform leapPoint;

    Collider2D playerToSpin;
    public float spinRange;

    public LayerMask whatIsPlayer;

    public float leapCoolDown;
    float leapCd;

    public float attackCoolDown;
    float attackCd;

    public float spinCoolDown;
    float spinCd;

    public float tauntCoolDown;
    float tauntCd;
    public GameObject thunder;

    public GameObject priceStone;

    public GameObject portal, nextChapter;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        canChase = true;
        canFace = true;
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        playerToLeap = Physics2D.OverlapCircle(leapPoint.position, attackRange, whatIsPlayer);
        playerToSpin = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + .5f), spinRange, whatIsPlayer);

        Chase();
        AttackPrep();
        Daze();
        Death();
    }

    void Chase()
    {
        if (canFace)
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

        if (playerToDamage == null && canChase && !attacking)
        {
            if (chaseCd <= 0)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                animator.SetBool("walking", true);
            }
            else
            {
                chaseCd -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    void AttackPrep()
    {
        Attack();
        LeapAttack();
        SpinAttack();
        TauntAttack();
    }

    void Attack()
    {
        if (playerToDamage != null && !attacking && attackCd <= 0)
        {
            attacking = true;
            animator.SetBool("attackBool", true);
            animator.SetTrigger("attack");
            chaseCd = chaseCoolDown;
            canFace = false;
        }
        else if(playerToDamage != null)
        {
            attackCd -= Time.deltaTime;
        }
    }

    public void AttackDamage()
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
        }
    }

    public void AttackEnd()
    {
        if (takeDamage.currentHealth <= takeDamage.health / 2)
        {
            attackCd = attackCoolDown/2;
        }
        else
        {
            attackCd = attackCoolDown;
        } 
        animator.SetBool("attackBool", false);
        attacking = false;
        canChase = true;
        canFace = true;
        chaseCd = chaseCoolDown;
    }

    void LeapAttack()
    {
        if (leapCd <= 0 && playerToLeap != null && !attacking)
        {
            attacking = true;
            animator.SetBool("leapAttackBool", true);
            animator.SetTrigger("leapAttack");
            chaseCd = chaseCoolDown;
            canFace = false;
            col2d.enabled = false;
        }
        else if(leapCd > 0)
        {
            leapCd -= Time.deltaTime;
        }
    }

    public void LeapDamage()
    {
        if (playerToLeap != null)
        {
            if (!PlayerMovement.blocking)
            {
                playerToLeap.GetComponent<PlayerMovement>().TakeDamage(damage);
                if (playerToLeap.transform.position.x > transform.position.x)
                {
                    PlayerMovement.dazeRight = true;
                }
                else if (playerToLeap.transform.position.x < transform.position.x)
                {
                    PlayerMovement.dazeRight = false;
                }
            }   
        }
    }

    public void LeapAttackEnd()
    {
        if (takeDamage.currentHealth <= takeDamage.health / 2)
        {
            leapCd = leapCoolDown / 2;
        }
        else
        {
            leapCd = leapCoolDown;
        }  
        animator.SetBool("leapAttackBool", false);
        attacking = false;
        gameObject.transform.position = new Vector2(leapPoint.position.x, transform.position.y);
        canChase = true;
        canFace = true;
        chaseCd = chaseCoolDown;
        col2d.enabled = true;
    }

    void SpinAttack()
    {
        if (spinCd <= 0 && playerToDamage != null && !attacking)
        {
            attacking = true;
            animator.SetBool("spinAttackBool", true);
            animator.SetTrigger("spinAttack");
            chaseCd = chaseCoolDown;
            canFace = false;
        }
        else if (spinCd > 0)
        {
            spinCd -= Time.deltaTime;
        }
    }

    public void SpinDamage()
    {
        if (playerToSpin != null)
        {
            playerToSpin.GetComponent<PlayerMovement>().TakeDamage(damage);
            if (playerToSpin.transform.position.x > transform.position.x)
            {
                PlayerMovement.dazeRight = true;
            }
            else if (playerToSpin.transform.position.x < transform.position.x)
            {
                PlayerMovement.dazeRight = false;
            }
        }
    }

    public void SpinEnd()
    {
        if (takeDamage.currentHealth <= takeDamage.health / 2)
        {
            spinCd = spinCoolDown / 2;
        }
        else
        {
            spinCd = spinCoolDown;
        }
        
        animator.SetBool("spinAttackBool", false);
        attacking = false;
        gameObject.transform.position = new Vector2(attackPos.position.x, transform.position.y);
        canChase = true;
        canFace = true;
        chaseCd = chaseCoolDown;
    }

    void TauntAttack()
    {
        if (tauntCd >= tauntCoolDown)
        {
            animator.SetTrigger("taunt");
            animator.SetBool("tauntBool", true);
            tauntCd = 0f;
            attacking = true;
            canFace = false;
        }
        else if (!attacking)
        {
            tauntCd += Time.deltaTime;
        }
    }

    public void CreateThunder()
    {
        Instantiate(thunder, new Vector2(player.transform.position.x, player.transform.position.y + 1f), Quaternion.identity);
    }

    public void TauntEnd()
    {
        if (takeDamage.currentHealth <= takeDamage.health / 2)
        {
            tauntCd = 1.5f;
        }
        else
        {
            tauntCd = 0f;
        }
        
        animator.SetBool("tauntBool", false);
        attacking = false;
        canChase = true;
        canFace = true;
        chaseCd = chaseCoolDown;
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
            sprite.color = new Color(.5f, .5f, .5f, 1);
            if (takeDamage.dazedTime > 0 && !attacking)
            {
                takeDamage.dazedTime -= Time.deltaTime;
                transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
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
            vikingDead.SetActive(true);
            Instantiate(vikingDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }

            portal.SetActive(true);
            nextChapter.SetActive(true);
            Instantiate(priceStone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(leapPoint.position, attackRange);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + .5f), spinRange);
    }
}
