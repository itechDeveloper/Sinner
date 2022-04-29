using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public GameObject destroyingEffect;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    private Animator animator;
    bool attacked;
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimation();
        Facing();
    }

    void AttackAnimation()
    {
        if (!attacked)
        {
            animator.SetBool("attack",true);           
        }
    }

    public void Attack()
    {
        WarriorManager.closestEnemy.GetComponent<TakeDamage>().GetDamage(damage);
        attacked = true;
    }

    public void Destroy()
    {
        Instantiate(destroyingEffect, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        Destroy(gameObject);
    }

    void Facing()
    {
        if (WarriorManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
