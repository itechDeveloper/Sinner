using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageController : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    public float speed;

    public float attackRangeX;
    public float attackRangeY;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;
    public GameObject spell;

    Collider2D playerToDamage;
    bool canFlip;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public int damage;

    private TakeDamage takeDamage;

    void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        readyAttackTime = startReadyAttackTime;
        canFlip = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(transform.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        AttackPrep();
        Chase();
    }

    void AttackPrep()
    {
        if (playerToDamage != null)
        {
            if (playerToDamage.transform.position.x < transform.position.x && canFlip)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (playerToDamage.transform.position.x > transform.position.x && canFlip)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (takeDamage.hit)
            {
                animator.SetBool("attacking", false);
                readyAttackTime = startReadyAttackTime;
                canFlip = true;
            }

            if (readyAttackTime <= 0)
            {
                animator.SetBool("attacking", true);
                canFlip = false;
            }
            else
            {
                animator.SetBool("attacking", false);
                readyAttackTime -= Time.deltaTime;
                canFlip = true;
            }
        }
    }

    public void Attack()
    {
        Instantiate(spell, attackPos.position, Quaternion.identity);
    }

    public void AttackEnd()
    {
        animator.SetBool("attacking", false);
        readyAttackTime = startReadyAttackTime;
        canFlip = true;
    }

    void Chase()
    {
        if (playerToDamage == null)
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

        if (playerToDamage == null && Mathf.Abs(transform.position.x - player.transform.position.x) < 20f && Mathf.Abs(transform.position.y - player.transform.position.y) < 2f)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(attackRangeX, attackRangeY));
    }
}
