using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMobController : MonoBehaviour
{
    Animator animator;
    private Patrol patrol;
    private TakeDamage takeDamage;

    private SpriteRenderer sprite;
    float dazeSpeed;

    public GameObject archerDead;

    internal RaycastHit2D dazeInfo;
    public Transform dazeDetection;
    public float distance;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        takeDamage = GetComponent<TakeDamage>();
    }

    void Update()
    {
        dazeInfo = Physics2D.Raycast(dazeDetection.position, Vector2.down, distance);
        Animate();
        Daze();
        Death();
    }

    void Animate()
    {
        animator.SetBool("walking", patrol.patrolMovement);
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
            patrol.canPatrol = false;
            sprite.color = new Color(.5f, .5f, .5f, 1);
            if (takeDamage.dazedTime > 0 && dazeInfo.collider)
            {
                dazeSpeed = 1f;
                takeDamage.dazedTime -= Time.deltaTime;
                transform.Translate(new Vector2(-dazeSpeed, 0) * Time.deltaTime);
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
        patrol.canPatrol = true;
        sprite.color = new Color(1, 1, 1, 1);
        takeDamage.dazed = false;
    }

    void Death()
    {
        if (takeDamage.dead)
        {
            archerDead.SetActive(true);
            Instantiate(archerDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                MeleeDead.facingRight = true;
            }
            else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                MeleeDead.facingRight = false;
            }
            Destroy(gameObject);
        }
    }
}
