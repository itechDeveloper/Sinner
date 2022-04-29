using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearsManager : MonoBehaviour
{
    public static bool canAttack;
    public int damage;
    bool gotDamage;

    Animator animator;
    internal bool canAnimate;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canAnimate)
        {
            animator.SetTrigger("attack");
        }
    }

    public void CanAttack()
    {
        canAttack = true;
    }

    public void CantAttack()
    {
        canAttack = false;
        gotDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canAttack && canAnimate)
            {
                collision.GetComponent<PlayerMovement>().TakeDamage(damage);
                gotDamage = true;
            }
        }   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canAttack && !gotDamage && canAnimate)
            {
                collision.GetComponent<PlayerMovement>().TakeDamage(damage);
                gotDamage = true;
            }
        }
    }
}