using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Manager : MonoBehaviour
{
    private GameObject player;
    Animator animator;
    public TakeDamage takeDamage;

    public float speed;
    public float chaseCoolDown;
    float chaseCd;
    bool canFace;
    bool canChase;

    public int damage;
    bool attacking;
    Collider2D playerToDamage;
    Collider2D playerToSelfDamage;
    public Transform attackPos;
    public Transform attackSelfPos;
    public float attackRange;
    public float attackSelfRange;
    public LayerMask whatIsPlayer;

    public float attackCoolDown;
    float attackCd;

    public GameObject bulletForever;
    public static int canFire;
    public bool canFireBullet;

    public GameObject bullet;
    public float startFireCd;
    float fireCd;

    public GameObject summonPoint1;
    public GameObject summonPoint2;

    private SpriteRenderer sprite;
    public GameObject fourDead;

    public GameObject priceStone;

    public GameObject portal, nextChapter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        summonPoint1 = GameObject.FindGameObjectWithTag("SummonPoint1");
        summonPoint2 = GameObject.FindGameObjectWithTag("SummonPoint2");
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        canChase = true;
        canFace = true;
        canFire = 2;
        canFireBullet = true;
        fireCd = startFireCd;
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        playerToSelfDamage = Physics2D.OverlapCircle(attackSelfPos.position, attackSelfRange, whatIsPlayer);

        Chase();
        Attack();
        FireBulletForever();
        Fire();
        SelfAttack();
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
            if (chaseCd <= 0 && Mathf.Abs(player.transform.position.x - transform.position.x) > .2f)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                animator.SetBool("walking", true);
            }
            else
            {
                animator.SetBool("walking", false);
                chaseCd -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("walking", false);
        }
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

    public void FireBulletForever()
    {  
        if (canFire == 2 && canFireBullet && !attacking)
        {
            animator.SetTrigger("fireBulletForever");
            animator.SetBool("fireBulletForeverBool", true);
            attacking = true;
            canFace = false;
            canFireBullet = false;

            if (transform.position.x > player.transform.position.x && canFire == 2)
            {
                BulletForever.rightBullet = true;
            }
            else if (transform.position.x < player.transform.position.x && canFire == 2)
            {
                BulletForever.rightBullet = false;
            }
        }
        else if (canFire < 2 && !canFireBullet)
        {
            canFireBullet = true;
        }
    }

    public void InstantiateBulletForever()
    {
        Instantiate(bulletForever, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        canFire--;
    }

    public void BulletForeverEnd()
    {
        animator.SetBool("fireBulletForeverBool", false);
        attacking = false;
        canFace = true;
    }

    void Fire()
    {
        if (fireCd <= 0 && !attacking)
        {
            animator.SetTrigger("fire");
            animator.SetBool("fireBool", true);
            attacking = true;
            fireCd = startFireCd;
            canFace = false;
            canChase = false;

            if (transform.position.x > player.transform.position.x)
            {
                BulletManager.rightBullet = false;
            }
            else
            {
                BulletManager.rightBullet = true;
            }
        }
        else
        {
            fireCd -= Time.deltaTime;
        }
    }

    public void InstantiateBullet()
    {
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
    }

    public void FireEnd()
    {
        animator.SetBool("fireBool", false);
        attacking = false;
        canFace = true;
        canChase = true;
    }

    void SelfAttack()
    {
        if (takeDamage.hit && !attacking)
        {
            animator.SetTrigger("selfDamage");
            animator.SetBool("selfDamageBool", true);
            attacking = true;
            canFace = false;
            canChase = false;
            takeDamage.hit = false;
        }
    }

    public void SelfDamage()
    {
        if (playerToSelfDamage != null)
        {
            playerToSelfDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
            if (playerToSelfDamage.transform.position.x > transform.position.x)
            {
                PlayerMovement.dazeRight = true;
            }
            else if (playerToSelfDamage.transform.position.x < transform.position.x)
            {
                PlayerMovement.dazeRight = false;
            }
        }
    }

    public void SummonBack()
    {
        animator.SetBool("selfDamageBool", false);
        animator.SetTrigger("summonBack");
        animator.SetBool("summonBackBool", true);

        if (Mathf.Abs(transform.position.x - summonPoint1.transform.position.x) < Mathf.Abs(transform.position.x - summonPoint2.transform.position.x))
        {
            transform.position = new Vector2(summonPoint2.transform.position.x - 2f, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(summonPoint1.transform.position.x + 2f, transform.position.y);
        }
    }

    public void SummonBackEnd()
    {
        takeDamage.hit = false;
        animator.SetBool("summonBackBool", false);
        attacking = false;
        canFace = true;
        canChase = true;
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
            fourDead.SetActive(true);
            Instantiate(fourDead, transform.position, Quaternion.identity);
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
            Instantiate(priceStone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(attackSelfPos.position, attackSelfRange);
    }
}
