using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public GameObject destroyingEffect;
    private Animator animator;
    bool attacked;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AttackAnimation();
        Facing();
    }

    void AttackAnimation()
    {
        if (!attacked)
        {
            animator.SetBool("attack", true);
        }
    }

    public void Attack()
    {
        if (NinjaManager.targetEnemy != null)
        {
            NinjaManager.targetEnemy.GetComponent<TakeDamage>().Destroyed();
        }
        attacked = true;
    }

    public void Destroy()
    {
        Instantiate(destroyingEffect, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        Destroy(gameObject);
    }

    void Facing()
    {
        if (NinjaManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
