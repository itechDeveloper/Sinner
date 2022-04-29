using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageAttack : MonoBehaviour
{
    private Animator animator;

    public float attackRangeX;
    public float attackRangeY;

    internal bool attacking;
    float readyAttackTime;
    public float startReadyAttackTime;
    public GameObject spell;

    Collider2D playerToDamage;
    int attackCounter;
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
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapBox(transform.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        AttackPrep();
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
                attackCounter = 0;
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
                attackCounter = 0;
                animator.SetBool("attacking", false);
                readyAttackTime -= Time.deltaTime;
                canFlip = true;
            }
        }
    }

    public void Attack()
    {
        Instantiate(spell, attackPos.position, Quaternion.identity);
        attackCounter++;
    }

    public void AttackEnd()
    {
        if (attackCounter > 5)
        {
            animator.SetBool("attacking", false);
            readyAttackTime = startReadyAttackTime;
            attackCounter = 0;
            canFlip = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(attackRangeX, attackRangeY));
    }
}
