using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingController : MonoBehaviour
{
    Animator animator;
    private Patrol patrol;
    private TakeDamage takeDamage;
    private VikingAttack meleeAttack;

    private SpriteRenderer sprite;
    float dazeSpeed;

    public GameObject meleeDead;

    internal RaycastHit2D dazeInfo;
    public Transform dazeDetection;
    public float distance;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        takeDamage = GetComponent<TakeDamage>();
        meleeAttack = GetComponent<VikingAttack>();
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
        if (takeDamage.dazed && !meleeAttack.canRoll)
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
            meleeDead.SetActive(true);
            Instantiate(meleeDead, transform.position, Quaternion.identity);
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
