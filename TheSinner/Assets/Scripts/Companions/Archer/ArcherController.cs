using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public GameObject arrow;
    public GameObject destroyingEffect;
    public GameObject arrowPosition;
  
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
            animator.SetBool("attack", true);
            attacked = true;
        }
    }

    public void Attack()
    {
        Instantiate(arrow, arrowPosition.transform.position, Quaternion.identity);
    }

    public void Destroy()
    {
        Instantiate(destroyingEffect, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        Destroy(gameObject);
    }

    void Facing()
    {
        if (ArcherManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
