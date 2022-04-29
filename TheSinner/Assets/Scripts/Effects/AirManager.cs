using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirManager : MonoBehaviour
{
    private GameObject player;
    public float speed;
    float lifetime;

    private Animator animator;

    public static int airCounter;
    int x;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        lifetime = 8f;
        x = airCounter;
        airCounter++;
    }

    
    void Update()
    {
        if (airCounter > 1 && x == 0 || MonkManManager.monkDead)
        {
            Destroy(gameObject);
        }

        if (!player.GetComponent<PlayerMovement>().daze)
        {
            if (player.transform.position.x > transform.position.x)
            {
                if (MonkManManager.activateUltimate)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime / 2);
                }
                else
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                
                PlayerMovement.dazeRight = true;
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                PlayerMovement.dazeRight = false;
            }
        }

        if (lifetime <= 0 && !MonkManManager.activateUltimate)
        {
            airCounter--;
            animator.SetBool("end", true);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }
}
