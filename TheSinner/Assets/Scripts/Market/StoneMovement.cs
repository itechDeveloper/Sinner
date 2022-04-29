using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    public bool canJump;

    public bool isGrounded;
    public Transform groundCheck;
    private float checkRadius;
    public LayerMask whatIsGround;
    int jumpCounter;

    public float jumpForce;

    void Start()
    {
        canJump = true;
        jumpCounter = 7;
        checkRadius = .1f;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (canJump && isGrounded)
        {
            jumpForce = jumpCounter * .4f;
            jumpCounter--;
        }

        if (canJump)
        {
            transform.Translate(Vector2.up * jumpForce * Time.deltaTime);
            jumpForce -= Time.deltaTime * 5;
        }

        if(jumpCounter <= 0)
        {
            canJump = false;
            jumpForce = 0;
        }
    }
}
