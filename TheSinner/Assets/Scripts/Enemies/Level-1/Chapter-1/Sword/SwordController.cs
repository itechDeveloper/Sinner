using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    Animator animator;
    private Patrol patrol;
    private TakeDamage takeDamage;
    private MeleeAttack meleeAttack;

    private SpriteRenderer sprite;
    float dazeSpeed;

    public GameObject swordDead;

    internal RaycastHit2D dazeInfo;
    public Transform dazeDetection;
    public float distance;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        takeDamage = GetComponent<TakeDamage>();
        meleeAttack = GetComponent<MeleeAttack>();
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
        if (patrol.patrolMovement || meleeAttack.canChase)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
       
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
            patrol.canPatrol = false;
            sprite.color = new Color(.5f, .5f, .5f, 1);
            if (takeDamage.dazedTime > 0 && !meleeAttack.attacking && dazeInfo.collider)
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
            swordDead.SetActive(true);
            Instantiate(swordDead, transform.position, Quaternion.identity);
            if (transform.eulerAngles == new Vector3(0,0,0))
            {
                SwordDead.facingRight = true;
            }else if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                SwordDead.facingRight = false;
            }
            Destroy(gameObject);
        }
    }
}
