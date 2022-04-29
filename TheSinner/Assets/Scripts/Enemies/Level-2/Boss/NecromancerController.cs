using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerController : MonoBehaviour
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
    public GameObject ghostWolf;
    public Transform wolfSummonPoint;
    public GameObject laveBall;
    public Transform[] laveBallPoints;
    int i;
    public GameObject smoke;
    public Transform[] smokePoints;
    public GameObject sword;

    public GameObject necroDead;
    int hitCounter;

    public static bool deadOnce;

    private GameObject lavePoint1;
    private GameObject lavePoint2;
    private GameObject lavePoint3;
    private GameObject smokePoint1;
    private GameObject smokePoint2;
    private GameObject smokePoint3;

    public GameObject priceStone;

    public GameObject portal, nextChapter;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        takeDamage = GetComponent<TakeDamage>();
        sprite = GetComponent<SpriteRenderer>();
        canChase = true;
        hitCounter = 0;
        attackCd = startAttackCd;
        meleeCd = startMeleeCd;
        lavePoint1 = GameObject.FindGameObjectWithTag("LavePoint1");
        lavePoint2 = GameObject.FindGameObjectWithTag("LavePoint2");
        lavePoint3 = GameObject.FindGameObjectWithTag("LavePoint3");
        smokePoint1 = GameObject.FindGameObjectWithTag("SmokePoint1");
        smokePoint2 = GameObject.FindGameObjectWithTag("SmokePoint2");
        smokePoint3 = GameObject.FindGameObjectWithTag("SmokePoint3");

        laveBallPoints[0] = lavePoint1.transform;
        laveBallPoints[1] = lavePoint2.transform;
        laveBallPoints[2] = lavePoint3.transform;
        smokePoints[0] = smokePoint1.transform;
        smokePoints[1] = smokePoint2.transform;
        smokePoints[2] = smokePoint3.transform;

        if (deadOnce)
        {
            takeDamage.currentHealth = takeDamage.health / 3;
        }
    }

    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);
        AttackPrep();
        Chase();
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
        if ((attackCd <= 0 || hitCounter > 3) && !attacking)
        {
            canChase = false;
            canFace = false; 
            attacking = true;
            if (attackType % 2 == 0)
            {
                animator.SetTrigger("cast");
                animator.SetBool("casting",true);                
                attackType++;
            }else if (attackType == 1)
            {
                animator.SetTrigger("laveCast");
                animator.SetBool("laveCastBool", true);
                attackType++;
            }else if (attackType == 3)
            {
                animator.SetTrigger("smokeCast");
                animator.SetBool("smokeCastBool", true);
                attackType++;
            }else if (attackType == 5)
            {
                animator.SetTrigger("swordCast");
                animator.SetBool("swordCastBool", true);
                attackType = 0;
            }

            hitCounter = 0;
        }else if (meleeCd <= 0 && !attacking && playerToDamage != null)
        {
            animator.SetBool("attacking", true);
            canChase = false;
            canFace = false;
            attacking = true;
        }
        else if(!attacking)
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
        attacking = false;
        meleeCd = startMeleeCd;
        chaseCd = startChaseCd;
    }

    public void SummonWolf()
    {
        Instantiate(ghostWolf, wolfSummonPoint.position, Quaternion.identity);
    }

    public void SummonWolfEnd()
    {
        canChase = true;
        canFace = true;
        attacking = false;
        animator.SetBool("casting", false);
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
    }

    public void InstantiateLaveBall()
    {
        if (takeDamage.currentHealth > takeDamage.health * 2 / 3) {
            if (i == 1)
            {
                Instantiate(laveBall, laveBallPoints[i].position, Quaternion.identity);
            }
        }else if (takeDamage.currentHealth > takeDamage.health / 3)
        {
            if (i == 0 || i == 2)
            {
                Instantiate(laveBall, laveBallPoints[i].position, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(laveBall, laveBallPoints[i].position, Quaternion.identity);
        }

        i++;
        if (i >= laveBallPoints.Length)
        {
            i = 0;
        }
    }

    public void LaveAttackEnd()
    {
        canChase = true;
        canFace = true;
        attacking = false;
        animator.SetBool("laveCastBool", false);
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
    }

    public void InstantiateSmoke()
    {
        for (int j = 0; j < smokePoints.Length; j++)
        {
            if (takeDamage.currentHealth > takeDamage.health * 2 / 3)
            {
                if (j == 1)
                {
                    Instantiate(smoke, smokePoints[j].position, Quaternion.identity);
                }
            }else if (takeDamage.currentHealth > takeDamage.health / 3)
            {
                if (j == 0 || j == 2)
                {
                    Instantiate(smoke, smokePoints[j].position, Quaternion.identity);
                }
            }
            else
            {
                Instantiate(smoke, smokePoints[j].position, Quaternion.identity);
            }
        }
    }

    public void EndSmokeAttack()
    {
        canChase = true;
        canFace = true;
        attacking = false;
        animator.SetBool("smokeCastBool", false);
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
    }

    public void InstantiateSword()
    {
        Instantiate(sword, new Vector2(player.transform.position.x, 3f), Quaternion.identity);
    }

    public void EndSwordAttack()
    {
        canChase = true;
        canFace = true;
        attacking = false;
        attackCd = startAttackCd;
        chaseCd = startChaseCd;
        animator.SetBool("swordCastBool", false);
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
            necroDead.SetActive(true);
            Instantiate(necroDead, transform.position, Quaternion.identity);
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
    }
}
