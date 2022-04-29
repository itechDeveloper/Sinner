using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForever : MonoBehaviour
{
    public float speed;
    public int damage;
    public static bool rightBullet;

    float lifeTime;

    public float point1;
    public float point2;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!rightBullet)
        {
            speed = -speed;
            transform.localScale = new Vector3(-1, 1, 1);
            rightBullet = true;
        }
        else
        {
            rightBullet = false;     
        }

        lifeTime = 10f;
    }

    void Update()
    {
        transform.Translate(speed * Vector2.right * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Boss4Manager.canFire++;
            Destroy(gameObject);
        }

       if (transform.position.x < point1)
       {
           speed *= -1;
           transform.localScale = new Vector3(1, 1, 1);
       }else if(transform.position.x > point2)
       {
           speed *= -1;
           transform.localScale = new Vector3(-1, 1, 1);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.GetComponent<PlayerMovement>().rollingCoolDown < 1f)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerMovement>().TakeDamage(damage);
                Boss4Manager.canFire++;
                Destroy(gameObject);
            }
        }
    }
}
