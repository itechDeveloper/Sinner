using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;
    float waitTime;
    internal bool canPatrol;
    internal bool patrolMovement;
    internal RaycastHit2D groundInfo;
    void Start()
    {
        waitTime = 1f;
        canPatrol = true;
    }

    void Update()
    {
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        PatrolMovement();  
    }

    void PatrolMovement()
    {
        if (canPatrol)
        {
            if (!groundInfo.collider)
            {
                if (waitTime > 0)
                {
                    waitTime -= Time.deltaTime;
                    patrolMovement = false;
                }
                else
                {
                    if (movingRight)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingRight = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingRight = true;
                    }

                    waitTime = 1f;
                }
            }
            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                patrolMovement = true;
            }
        }
    }
}
