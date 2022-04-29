using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour
{
    private TakeDamage takeDamage;
    private SpriteRenderer sprite;
    public GameObject warlockDead;

    public float speed;
    public int damage;

    bool canChase;
    bool canFace;
    public float chaseCoolDown;
    float chaseCd;

    private Animator animator;
    private GameObject player;

    public LayerMask whatIsPlayer;

    bool attacking;
    Collider2D playerToDamage;
    public Transform attackPos;
    public float attackRange;

    public float attackCoolDown;
    float attackCd;

    float spellCd;
    public float startSpellCd;

    public GameObject[] bosses;
    int i;

    public GameObject summonEffect;

    public GameObject[] backgrounds;
    int j;

    void Start()
    {
        spellCd = startSpellCd;
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        canChase = true;
        canFace = true;
        i = 0;
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        Chase();
        Attack();
        Daze();
        SpellBoss();
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

    void SpellBoss()
    {
        if (spellCd <= 0 && !attacking)
        {
            animator.SetTrigger("spellCast");
            animator.SetBool("spellCasting", true);
            attacking = true;
            canFace = false;
            canChase = false;

            j++;
            if (j == backgrounds.Length)
            {
                j = 0;
            }
            
            backgrounds[j].SetActive(true);
            for (int k = 0; k < backgrounds.Length; k++)
            {
                if (k != j)
                {
                    backgrounds[k].SetActive(false);
                }
            }
        }
        else
        {
            spellCd -= Time.deltaTime;
        }
    }

    public void SpellCast()
    {
        if (i > bosses.Length - 1)
        {
            i = 0;
        }
        Instantiate(summonEffect, new Vector2(transform.position.x + 1.9f, transform.position.y + 1.25f), Quaternion.identity);

        if (i == 3)
        {
            Instantiate(bosses[i], new Vector2(transform.position.x + 2f, transform.position.y - .35f), Quaternion.identity);
        }
        else
        {
            Instantiate(bosses[i], new Vector2(transform.position.x + 2f, transform.position.y), Quaternion.identity);
        }
        i++;
    }

    public void SpellCastEnd()
    {
        animator.SetBool("spellCasting", false);
        if (takeDamage.currentHealth < takeDamage.health * 3 / 4 && takeDamage.currentHealth > takeDamage.health / 2)
        {
            spellCd = startSpellCd * 3 / 4;
        }else if (takeDamage.currentHealth < takeDamage.health / 2 && takeDamage.currentHealth > takeDamage.health * 1 / 4)
        {
            spellCd = startSpellCd / 2;
        }else if (takeDamage.currentHealth < takeDamage.health / 4)
        {
            spellCd = startSpellCd / 4;
        }
        else
        {
            spellCd = startSpellCd;
        }
        attacking = false;
        canFace = true;
        canChase = true;
        chaseCd = chaseCoolDown;
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
        else if (playerToDamage != null)
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
            attackCd = attackCoolDown / 2;
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
            warlockDead.SetActive(true);
            Instantiate(warlockDead, transform.position, Quaternion.identity);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
