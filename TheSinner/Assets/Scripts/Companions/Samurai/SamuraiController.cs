using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject destroyingEffect;
    private Animator animator;
    bool attacked;
    public int damage;

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
        }
    }

    public void Attack()
    {
        for (int i = 0; i < SamuraiManager.enemies.Length ; i++)
        {
            if (SamuraiManager.enemies[i] != null)
            {
                SamuraiManager.enemies[i].GetComponent<TakeDamage>().GetDamage(damage);
                Instantiate(bloodEffect, new Vector2(SamuraiManager.enemies[i].transform.position.x + .7f, SamuraiManager.enemies[i].transform.position.y -.1f) , Quaternion.identity);
            }
            attacked = true;
        }  
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
