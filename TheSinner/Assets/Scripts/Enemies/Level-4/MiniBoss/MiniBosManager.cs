using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBosManager : MonoBehaviour
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
    int attackType;

    public GameObject evilDead;
    bool spelling;

    public GameObject spell;
    int spellCounter;
    float spellCountDown;

    public GameObject[] spears;
    int i = 0;

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
        spellCountDown = .5f;
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        Chase();
        AttackPrep();
        Daze();
        Death();
        InstantiateSpell();
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
        if (attackCd <= 0 && !attacking)
        {
            canChase = false;
            canFace = false;
            attacking = true;
            takeDamage.hit = false;
            spelling = true;
            if (attackType == 0)
            {
                animator.SetTrigger("spell");
                animator.SetBool("spelling", true);
                attackType++;
            }
            else if (attackType == 1)
            {
                animator.SetTrigger("summon");
                animator.SetBool("summoning", true);
                attackType--;
            }
        }

        else if(takeDamage.hit && !attacking)
        {
            animator.SetBool("attacking", true);
            canChase = false;
            canFace = false;
            attacking = true;
            takeDamage.hit = false;
        }
         
        else if (meleeCd <= 0 && !attacking && playerToDamage != null)
        {
            animator.SetBool("attacking", true);
            canChase = false;
            canFace = false;
            attacking = true;
        }
        else if (!attacking)
        {
            canChase = true;
            canFace = true;
            meleeCd -= Time.deltaTime;
        }

        if (!spelling)
        {
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
        attacking = false;
        meleeCd = startMeleeCd;
        chaseCd = startChaseCd;
    }

    public void SpellEnd()
    {
        animator.SetBool("spelling", false);
        canFace = true;
        canChase = true;
        attacking = false;
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
        spelling = false;
    }
    
    public void ResetSpellCounter()
    {
        spellCounter = 7;
    }

    void InstantiateSpell()
    {
        if (player.GetComponent<PlayerMovement>().isGrounded)
        {
            if (spellCounter > 0 && spellCountDown <= 0f)
            {
                Instantiate(spell, new Vector2(player.transform.position.x , player.transform.position.y + 2.1f), Quaternion.identity);
                spellCounter--;
                spellCountDown = .5f;
            }
            else
            {
                spellCountDown -= Time.deltaTime;
            }
        }
    }

    public void SummonAttack()
    {
        spears[i].GetComponent<SpearsManager>().canAnimate = true;
        if (i < spears.Length)
        {
            i++;
        }
    }

    public void SummonEnd()
    {
        animator.SetBool("summoning", false);
        canFace = true;
        canChase = true;
        attacking = false;
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
        spelling = false;
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
