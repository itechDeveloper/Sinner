using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public int damage;
    bool canDamage;
    
    public void CanDamage()
    {
        canDamage = true;
    }

    public void CantDamage()
    {
        canDamage = false;
    }

    public void CloseLaser()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canDamage)
            {
                collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            }
        }
    }
}
