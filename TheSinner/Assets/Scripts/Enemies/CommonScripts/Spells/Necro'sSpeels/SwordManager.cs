using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    private GameObject player;
    public float chaseCd;
    public float Ypos;
    public float speed;
    private Animator animator;
    public float destroyY;
    public int damage;
    public GameObject sword;
    public GameObject destroyAnim;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Chase();
        Destroy();
    }

    void Chase()
    {
        if(chaseCd > 0)
        {
            transform.position =  Vector2.MoveTowards(transform.position, new Vector2 (player.transform.position.x, Ypos), speed * Time.deltaTime);
            chaseCd -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("attack", true);
            transform.Translate(Vector2.down * speed * 1.5f * Time.deltaTime);
            speed += Time.deltaTime * 10;
        }
    }

    void Destroy()
    {
        if (transform.position.y < destroyY)
        {
            Instantiate(sword, new Vector2(transform.position.x, destroyY), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Instantiate(destroyAnim, new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
