using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    private Animator animator;
    bool canChase;
    private GameObject player;
    bool canFace;
    private TakeDamage takeDamage;
    bool destroyable;
    public float lifeTime;
    public float speed;
    bool attacking;

    private Collider2D playerToDamage;
    public int damage;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public Transform attackPos;

    public GameObject skeletonSword;
    public GameObject skeletonArcher;
    public GameObject skeletonMage;
    public GameObject skeletonShield;
    public GameObject skeletonGhost;

    private GameObject necromancer;
    public GameObject deadAnim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        necromancer = GameObject.FindGameObjectWithTag("Necromancer");
        animator = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamage>();
        animator.SetBool("howling", true);
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemy);
        Face();
        Chase();
        AttackPrep();
        Destroy();
        if (!destroyable)
        {
            takeDamage.currentHealth = takeDamage.health;
        }
    }

    void Face()
    {
        if (canFace)
        {
            if (transform.position.x < player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0,-180,0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
        }
    }

    void Chase()
    {
        if (canChase)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    void AttackPrep()
    {
        if (playerToDamage != null)
        {
            animator.SetTrigger("attack");
            animator.SetBool("attackBool", true);
            attacking = true;
            canChase = false;
            canFace = false;
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
        animator.SetBool("attackBool", false);
        canChase = true;
        canFace = true;
        attacking = false;
    }

    public void Howl()
    {
        int randomNum;
        int randomX;
        for (int i = 0; i < 3; i++)
        {
            randomX = Random.Range(-10, 10);
            randomNum = Random.Range(0,5);
            if (necromancer.GetComponent<NecromancerController>().takeDamage.currentHealth > necromancer.GetComponent<NecromancerController>().takeDamage.health * 2 / 3)
            {
                if (i == 0)
                {
                    if (randomNum == 0)
                    {
                        Instantiate(skeletonSword, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else if (randomNum == 1)
                    {
                        Instantiate(skeletonShield, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else if (randomNum == 2)
                    {
                        Instantiate(skeletonArcher, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else if (randomNum == 3)
                    {
                        Instantiate(skeletonMage, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(skeletonGhost, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                }
            }else if (necromancer.GetComponent<NecromancerController>().takeDamage.currentHealth > necromancer.GetComponent<NecromancerController>().takeDamage.health * 2 / 3)
            {
                if (i < 3)
                {
                    if (randomNum == 0)
                    {
                        Instantiate(skeletonSword, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else if (randomNum == 1)
                    {
                        Instantiate(skeletonShield, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else if (randomNum == 2)
                    {
                        Instantiate(skeletonArcher, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else if (randomNum == 3)
                    {
                        Instantiate(skeletonMage, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(skeletonGhost, new Vector2(randomX, transform.position.y), Quaternion.identity);
                    }
                }
            }
            else
            {
                if (randomNum == 0)
                {
                    Instantiate(skeletonSword, new Vector2(randomX, transform.position.y), Quaternion.identity);
                }
                else if (randomNum == 1)
                {
                    Instantiate(skeletonShield, new Vector2(randomX, transform.position.y), Quaternion.identity);
                }
                else if (randomNum == 2)
                {
                    Instantiate(skeletonArcher, new Vector2(randomX, transform.position.y), Quaternion.identity);
                }
                else if (randomNum == 3)
                {
                    Instantiate(skeletonMage, new Vector2(randomX, transform.position.y), Quaternion.identity);
                }
                else
                {
                    Instantiate(skeletonGhost, new Vector2(randomX, transform.position.y), Quaternion.identity);
                }
            }
            
        }
    }

    public void HowlEnd()
    {
        destroyable = true;
        canChase = true;
        canFace = true;
        animator.SetBool("howling", false);
    }

    void Destroy()
    {
        if (!attacking)
        {
            lifeTime -= Time.deltaTime;
        }

        if ((takeDamage.hit || lifeTime <= 0) && destroyable)
        {
            Instantiate(deadAnim, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
