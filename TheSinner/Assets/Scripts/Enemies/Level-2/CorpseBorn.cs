using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBorn : MonoBehaviour
{
    private Animator animator;
    public GameObject skeleton;
    Collider2D wakeningPoint;
    public LayerMask whatIsToWake;
    public float wakeRangeX;
    public float wakeRangeY;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        BornController();
        wakeningPoint = Physics2D.OverlapBox(transform.position, new Vector2(wakeRangeX,wakeRangeY),0, whatIsToWake);
    }

    void BornController()
    {
        if (wakeningPoint != null)
        {
            animator.SetTrigger("born");
        }
    }

    public void Born()
    {
        Instantiate(skeleton, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(wakeRangeX, wakeRangeY));
    }
}
