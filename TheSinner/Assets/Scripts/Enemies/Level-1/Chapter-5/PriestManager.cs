using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestManager : MonoBehaviour
{
    public float speed;
    public int damage;
    private Animator animator;
    private GameObject player;
    Collider2D playerToDamage;
    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;

    float meleeCd;
    public float startMeleeCd;

    private TakeDamage takeDamage;
    public int healthPower;
    public float startHealthCd;
    bool canChase;
    bool canFace;
    float chaseCd;
    public float startChaseCd;
    bool attacking;
    bool healing;
    bool healedUp;

    public float dazeSpeed;

    private SpriteRenderer sprite;

    public GameObject priestDead;

    int hitCounter;
    public GameObject explosionEffect;
    public Transform [] points;

    public GameObject iceShard;
    public Transform[] icePoints;
    int j = 0;
    bool loopAttack;
    bool quarterHealth;

    public GameObject healthStone;

     public GameObject portal, nextChapter;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        takeDamage = GetComponent<TakeDamage>();
        sprite = GetComponent<SpriteRenderer>();
        canChase = true;
        canFace = true;
        loopAttack = true;
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        AttackPrep();
        Chase();
        Daze();
        Death();

        if (takeDamage.currentHealth <= takeDamage.health / 4 && !healedUp)
        {
            quarterHealth = true;
            healedUp = true;
        }
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

    void AttackPrep()
    {
        if (quarterHealth)
        {
            if (!attacking && !healing)
            {
                canChase = false;
                canFace = false;
                quarterHealth = false;
                healing = true;
                animator.SetTrigger("healLoop");
                animator.SetBool("healLoopBool", true);
                for (int i = 0; i < points.Length; i++)
                {
                    Instantiate(explosionEffect, points[i].transform.position, Quaternion.identity);
                }
            }
        }
        else if (!healing)
        {
            if (hitCounter >= 3 && !healing && !attacking)
            {
                canChase = false;
                canFace = false;
                animator.SetTrigger("heal");
                animator.SetBool("healBool", true);
                for (int i = 0; i < points.Length; i++)
                {
                    Instantiate(explosionEffect, points[i].transform.position, Quaternion.identity);
                }
                healing = true;
                hitCounter = 0;
            }
            else if (playerToDamage != null)
            {
                if (takeDamage.hit && !attacking && meleeCd > 0)
                {
                    if (takeDamage.currentHealth < takeDamage.health/2 && loopAttack)
                    {
                        animator.SetTrigger("attackLoop");
                        animator.SetBool("attackLoopBool", true);
                        meleeCd = startMeleeCd;
                        canChase = false;
                        canFace = false;
                        attacking = true;
                        loopAttack = false;
                    }
                    else
                    {
                        animator.SetTrigger("attackLoad");
                        animator.SetBool("attackLoadBool", true);
                        meleeCd = startMeleeCd;
                        canChase = false;
                        canFace = false;
                        attacking = true;
                        hitCounter++;
                        if (hitCounter > 3 || takeDamage.currentHealth < takeDamage.health / 2)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                Instantiate(explosionEffect, points[i].transform.position, Quaternion.identity);
                            }
                            hitCounter = 0;
                        }
                        loopAttack = true;
                    }
                }
                else if (meleeCd <= 0 && !attacking)
                {
                    if (takeDamage.currentHealth > takeDamage.health / 2)
                    {
                        animator.SetTrigger("attack");
                        animator.SetBool("attackBool", true);
                        meleeCd = startMeleeCd;
                        canChase = false;
                        canFace = false;
                        attacking = true;
                    }
                    else
                    {
                        animator.SetTrigger("attackLoop");
                        animator.SetBool("attackLoopBool", true);
                        meleeCd = startMeleeCd;
                        canChase = false;
                        canFace = false;
                        attacking = true;
                    }
                }
                else if (!attacking)
                {
                    meleeCd -= Time.deltaTime;
                }
            }
        }
    }

    public void InstantiateIce()
    {
        Instantiate(iceShard, icePoints[j].position, Quaternion.identity);
        j++;
        if (j >= icePoints.Length)
        {
            j = 0;
        }
    }

    public void LastIceAttack()
    {
        for (int i =0; i < icePoints.Length; i++)
        {
            Instantiate(iceShard, icePoints[i].position, Quaternion.identity);
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
        animator.SetBool("attackLoadBool", false);
        animator.SetBool("attackBool", false);
        animator.SetBool("attackLoopBool", false);
        attacking = false;
        chaseCd = startChaseCd;
    }

    public void HealUp()
    {
        if (takeDamage.currentHealth + healthPower > takeDamage.health)
        {
            takeDamage.currentHealth = takeDamage.health;
        }
        else
        {
            takeDamage.currentHealth += healthPower;
        }
    }

    public void HealEnd()
    {
        chaseCd = startChaseCd;
        canChase = true;
        canFace = true;
        animator.SetBool("healBool", false);
        animator.SetBool("healLoopBool", false);
        healing = false;
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
            sprite.color = new Color(.5f, .5f, .5f, 1);
            if (takeDamage.dazedTime > 0 && !attacking && !healing)
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
            priestDead.SetActive(true);
            Instantiate(priestDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }

            nextChapter.SetActive(true);
            portal.SetActive(true);
            Instantiate(healthStone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
