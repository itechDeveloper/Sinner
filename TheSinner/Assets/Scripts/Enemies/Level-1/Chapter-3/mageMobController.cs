using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageMobController : MonoBehaviour
{
    Animator animator;
    private TakeDamage takeDamage;

    private SpriteRenderer sprite;
    float dazeSpeed;

    public GameObject mageDead;

    internal RaycastHit2D dazeInfo;
    public Transform dazeDetection;
    public float distance;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamage>();
    }

    void Update()
    {
        dazeInfo = Physics2D.Raycast(dazeDetection.position, Vector2.down, distance);
        Daze();
        Death();
    }

    void Daze()
    {
        if (takeDamage.dazed)
        {
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
        sprite.color = new Color(1, 1, 1, 1);
        takeDamage.dazed = false;
    }

    void Death()
    {
        if (takeDamage.dead)
        {
            mageDead.SetActive(true);
            Instantiate(mageDead, transform.position, Quaternion.identity);
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
