using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DalgaManager : MonoBehaviour
{
    Color spirit;
    bool canDamage;
    public int damage;
    public static bool facingRight;

    void Start()
    {
        spirit = gameObject.GetComponent<SpriteRenderer>().color;
        spirit.a = .6f;
        gameObject.GetComponent<SpriteRenderer>().material.color = spirit;
        if (!facingRight)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    public void NormalizeColor()
    {
        spirit.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().material.color = spirit;
        canDamage = true;
    }

    public void ControlDamage()
    {
        if (spirit.a == 1)
        {
            canDamage = false;
        }
    }

    public void Destroy()
    {
        if (spirit.a == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canDamage)
            {
                collision.GetComponent<PlayerMovement>().TakeDamage(damage);
                if (facingRight)
                {
                    PlayerMovement.dazeRight = false;
                }
                else
                {
                    PlayerMovement.dazeRight = true;
                }
            }
        }
    }
}
