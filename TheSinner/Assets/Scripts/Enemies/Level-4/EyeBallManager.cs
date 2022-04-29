using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallManager : MonoBehaviour
{
    private Patrol patrol;
    private Animator animator;

    public float chaseRangeX;
    public float chaseRangeY;
    public Transform chasingPoint;

    bool canChase;
    Collider2D playerToChase;
    public LayerMask whatIsToChase;

    public LayerMask whatIsEnemies;
    public float readyAttackTime;
    internal bool attacking;

    private GameObject player;
    public GameObject explosion;
    void Start()
    {
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        playerToChase = Physics2D.OverlapBox(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector2(chaseRangeX, chaseRangeY), 0, whatIsToChase);
        Chasing();
        AttackPrep();
    }

    void AttackPrep()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f)
        {
            patrol.canPatrol = false;
            patrol.patrolMovement = false;
            canChase = false;

            if (readyAttackTime <= 0)
            {
                animator.SetBool("attacking", true);
                attacking = true;
            }
            else
            {
                readyAttackTime -= Time.deltaTime;
            }
        }
        else
        {
            canChase = true;
        }
    }

    void Chasing()
    {
        if (playerToChase != null && !attacking)
        {
            if (playerToChase.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (playerToChase.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (!patrol.groundInfo.collider && playerToChase != null && canChase)
        {
            transform.Translate(Vector2.zero);
            patrol.patrolMovement = false;
        }
        else if (playerToChase != null && canChase && patrol.groundInfo.collider)
        {
            patrol.canPatrol = false;
            animator.SetBool("walking", true);
            if (playerToChase.transform.position.x < transform.position.x && !attacking)
            {
                transform.Translate(Vector2.right * patrol.speed * Time.deltaTime);
            }
            else if (playerToChase.transform.position.x > transform.position.x && !attacking)
            {
                transform.Translate(Vector2.right * patrol.speed * Time.deltaTime);
            }
        }
        else if (playerToChase == null)
        {
            patrol.canPatrol = true;
            patrol.patrolMovement = true;
        }
    }

    public void Explode()
    {
        Instantiate(explosion, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(chasingPoint.position.x, chasingPoint.position.y + 1f), new Vector3(chaseRangeX, chaseRangeY, 0));
    }
}
