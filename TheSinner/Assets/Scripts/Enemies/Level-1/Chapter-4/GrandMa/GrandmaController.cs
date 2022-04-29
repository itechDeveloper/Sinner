using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    private TakeDamage takeDamage;
    private SpriteRenderer sprite;
    float dazeSpeed;

    public Transform point1;
    public Transform point2;

    bool movePoint1;
    bool canMove;

    private Animator animator;

    public LayerMask whatIsPlayer;
    public float attackRangeX;
    public float attackRangeY;
    public float attackRange;
    Collider2D areaDamage;
    Collider2D laserDamage;
    public int damage;
    bool attacking;

    float attackCoolDown;
    public float startAttackCoolDown;
    bool firstAttack;
    public GameObject bullet;

    bool laserAttacking;
    private GameObject player;

    public Transform laserPoint;
    public GameObject laserAnimation;

    public GameObject destroyEffect;
    public GameObject spawnEffect;

    public GameObject grandmaDead;

    public GameObject priceStone;
    void Start()
    {
        animator = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamage>();
        sprite = GetComponent<SpriteRenderer>();
        canMove = true;
        firstAttack = true;
        attackCoolDown = startAttackCoolDown;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        areaDamage = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
        laserDamage = Physics2D.OverlapBox(laserPoint.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsPlayer);
        Facing();
        CheckPoint();
        AreaAttack();
        BulletAttack();
        LaserAttack();
        Daze();
        Death();

        if (attackCoolDown > 0)
        {
            attackCoolDown -= Time.deltaTime;
        }
    }

    void Facing()
    {
        if (!laserAttacking)
        {
            if (transform.position.x > player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }else if (transform.position.x < player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    void CheckPoint()
    {
        if (Mathf.Abs(transform.position.x - point1.position.x) > 2f && Mathf.Abs(transform.position.x - point1.position.x) < 10f)
        {
            movePoint1 = false;
            MoveToPoint();
        }else if (Mathf.Abs(transform.position.x - point2.position.x) > 2f && Mathf.Abs(transform.position.x - point2.position.x) < 10f)
        {
            movePoint1 = true;
            MoveToPoint();
        }
    }

    void ChangePoint()
    {
        if (Mathf.Abs(transform.position.x - point1.position.x) < Mathf.Abs(transform.position.x - point2.position.x))
        {
            movePoint1 = false;
        }
        else
        {
            movePoint1 = true;
        }

        MoveToPoint();
    }

    void MoveToPoint()
    {
        if (canMove)
        {
            if (movePoint1)
            {
                Instantiate(destroyEffect, new Vector2(point2.position.x, point2.position.y + .5f), Quaternion.identity);
                Instantiate(spawnEffect, new Vector2(point1.position.x - .25f, point1.position.y + 1.25f), Quaternion.identity);
                transform.position = point1.position;
            }
            else
            {
                transform.position = point2.position;
                Instantiate(spawnEffect, new Vector2(point2.position.x - .25f, point2.position.y + 1.25f), Quaternion.identity);
                Instantiate(destroyEffect, new Vector2(point1.position.x, point1.position.y + .5f), Quaternion.identity);
            }
        }       
    }

    void AreaAttack()
    {
        if (!attacking)
        {
            if (takeDamage.hitCounter > 2)
            {
                animator.SetTrigger("areaDamage");
                animator.SetBool("areaDamageBool", true);
                takeDamage.hitCounter = 0;
                attacking = true;
                canMove = false;
            }
        }    
    }

    public void AreaAttackDamage()
    {
        if (areaDamage != null)
        {
            areaDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }

    public void AreaAttackEnd()
    {
        attacking = false;
        animator.SetBool("areaDamageBool", false);
        EndDaze();
        canMove = true;
        ChangePoint();
    }

    void BulletAttack()
    {
        if (!attacking)
        {
            if (attackCoolDown <= 0)
            {
                if (firstAttack)
                {
                    attacking = true;
                    animator.SetTrigger("bulletAttack");
                    animator.SetBool("bulletAttackBool", true);
                    attackCoolDown = startAttackCoolDown;
                    firstAttack = false;
                }
            }
        } 
    }

    public void InstantiateBullet()
    {
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + .5f) , Quaternion.identity);
    }

    public void EndBulletAttack()
    {
        EndDaze();
        ChangePoint();
        attacking = false;
        animator.SetBool("bulletAttackBool", false);
    }

    void LaserAttack()
    {
        if (!attacking)
        {
            if (attackCoolDown <= 0)
            {
                if (!firstAttack)
                {
                    attacking = true;
                    animator.SetTrigger("laserAttack");
                    animator.SetBool("laserAttackBool", true);
                    attackCoolDown = startAttackCoolDown;
                    firstAttack = true;
                    laserAttacking = true;
                }
            }
        }
    }

    public void LaserDamage()
    {
        if (laserDamage != null)
        {
            laserDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
        laserAnimation.SetActive(true);
    }

    public void EndLaserAttack()
    {
        EndDaze();
        ChangePoint();
        laserAnimation.SetActive(false);
        animator.SetBool("laserAttackBool", false);
        attacking = false;
        laserAttacking = false;
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
            grandmaDead.SetActive(true);
            Instantiate(grandmaDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }

            DeathControl.grandmaDead = true;
            Instantiate(priceStone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + .5f), attackRange);
        Gizmos.DrawWireCube(laserPoint.position, new Vector2(attackRangeX, attackRangeY));
    }
}
