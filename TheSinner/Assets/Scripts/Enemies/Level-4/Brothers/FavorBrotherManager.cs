using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavorBrotherManager : MonoBehaviour
{
    public float speed;
    public int damage;
    private Animator animator;
    private GameObject player;
    Collider2D playerToDamage;
    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;

    internal TakeDamage takeDamage;
    bool canChase;
    bool canFace;
    float chaseCd;
    public float startChaseCd;
    public float dazeSpeed;
    private SpriteRenderer sprite;

    bool attacking;
    float meleeCd;
    public float startMeleeCd;
    float attackCd;
    public float startAttackCd;
    int hitCounter;
    int attackType;

    public GameObject evilDead;

    int attackingType;

    public GameObject stun;
    public GameObject parryBall;

    public Transform chasePoint;
    public GameObject priceStone;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        takeDamage = GetComponent<TakeDamage>();
        sprite = GetComponent<SpriteRenderer>();
        canChase = true;
        attackCd = startAttackCd;
        meleeCd = startMeleeCd;
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        Chase();
        AttackPrep();
        Daze();
        Death();
    }

    void Chase()
    {
        if (Mathf.Abs(player.transform.position.x - chasePoint.position.x) < 20f && Mathf.Abs(player.transform.position.y - chasePoint.position.y) < 4f)
        {
            canChase = true;
        }
        else
        {
            canChase = false;
        }

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

        if (playerToDamage == null && canChase)
        {
            if (chaseCd <= 0)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                animator.SetBool("running", true);
            }
            else
            {
                chaseCd -= Time.deltaTime;
            }

        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    public void AttackPrep()
    {
        if ((attackCd <= 0 || hitCounter > 3) && !attacking)
        {
            canChase = false;
            canFace = false;
            attacking = true;
            if (attackType == 0)
            {
                animator.SetTrigger("parry");
                animator.SetBool("parrying", true);
                attackType++;
            }
            else if (attackType == 1)
            {
                animator.SetTrigger("stun");
                animator.SetBool("stunning", true);
                attackType--;
            }

            hitCounter = 0;
        }
        else if (meleeCd <= 0 && !attacking && playerToDamage != null)
        {
            if (attackingType == 0)
            {
                animator.SetBool("attacking", true);
                attackingType++;
            }
            else
            {
                animator.SetBool("attacking2", true);
                attackingType--;
            }
            canChase = false;
            canFace = false;
            attacking = true;
        }
        else if (!attacking)
        {
            canChase = true;
            canFace = true;
            meleeCd -= Time.deltaTime;
            attackCd -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (playerToDamage != null)
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

    public void AttackEnd()
    {
        canChase = true;
        canFace = true;
        animator.SetBool("attacking", false);
        animator.SetBool("attacking2", false);
        attacking = false;
        meleeCd = startMeleeCd;
        chaseCd = startChaseCd;
    }

    public void InstantiateParry()
    {
        Instantiate(parryBall, new Vector2(player.transform.position.x + Random.Range(-1,1), transform.position.y), Quaternion.identity);
    }

    public void ParryEnd()
    {
        animator.SetBool("parrying", false);
        canFace = true;
        canChase = true;
        attacking = false;
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
    }

    public void InstantiateStun()
    {
        if (transform.position.x > player.transform.position.x)
        {
            DalgaManager.facingRight = false;
        }
        else
        {
            DalgaManager.facingRight = true;
        }

        Instantiate(stun, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(stun, new Vector2(transform.position.x + 2f, transform.position.y), Quaternion.identity);
        Instantiate(stun, new Vector2(transform.position.x + 4f, transform.position.y), Quaternion.identity);
        Instantiate(stun, new Vector2(transform.position.x + 6f, transform.position.y), Quaternion.identity);
        Instantiate(stun, new Vector2(transform.position.x + 8f, transform.position.y), Quaternion.identity);
        Instantiate(stun, new Vector2(transform.position.x + 10f, transform.position.y), Quaternion.identity);
        Instantiate(stun, new Vector2(transform.position.x + 12f, transform.position.y), Quaternion.identity);
    }

    public void StunEnd()
    {
        animator.SetBool("stunning", false);
        canFace = true;
        canChase = true;
        attacking = false;
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
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
            evilDead.SetActive(true);
            Instantiate(evilDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }
            Instantiate(priceStone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
