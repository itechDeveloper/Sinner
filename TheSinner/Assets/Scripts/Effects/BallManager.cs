using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public int damage;
    public TakeDamage takeDamage;
    
    void Update()
    {
        if (takeDamage.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);

            if (collision.transform.position.x > transform.position.x)
            {
                PlayerMovement.dazeRight = true;
            }
            else
            {
                PlayerMovement.dazeRight = false;
            }
        }
    }
}
