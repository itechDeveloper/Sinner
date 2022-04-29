using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaveBallManager : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private TakeDamage takeDamage;
    public float speed;
    public int damage;

    private SpriteRenderer sprite;
    public float dazeSpeed;

    bool canChase;
    private CircleCollider2D cirCol2d;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamage>();
        sprite = GetComponent<SpriteRenderer>();
        canChase = true;
        cirCol2d = GetComponent<CircleCollider2D>();
    }

    
    void Update()
    {
        ChasePlayer();
        if (takeDamage.currentHealth <= 0)
        {
            cirCol2d.enabled = false;
            animator.SetTrigger("dead");
        }
        Daze();

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0,-180,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void ChasePlayer()
    {
        if (takeDamage.currentHealth > 0 && canChase)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + .5f), speed * Time.deltaTime);
        }        
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
            sprite.color = new Color(.5f, .5f, .5f, 1);
            if (takeDamage.dazedTime > 0)
            {
                takeDamage.dazedTime -= Time.deltaTime;
                transform.Translate(new Vector2(dazeSpeed, 0) * Time.deltaTime);
            }
            else
            {
                takeDamage.dazed = false;
            }
        }
    }

    public void EndDaze()
    {
        animator.SetBool("getHitBool", false);
        sprite.color = new Color(1, 1, 1, 1);
        takeDamage.dazed = false;
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<PlayerMovement>().hittable)
        {
            canChase = false;
            player.GetComponent<PlayerMovement>().TakeDamage(damage);
            cirCol2d.enabled = false;
            animator.SetTrigger("dead");
        }
    }
}
