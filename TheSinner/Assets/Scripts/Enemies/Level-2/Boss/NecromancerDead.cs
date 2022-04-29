using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerDead : MonoBehaviour
{
    private Animator animator;
    public GameObject necromancer;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        if (player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    public void CheckReborn()
    {
        if (!NecromancerController.deadOnce)
        {
            NecromancerController.deadOnce = true;
            animator.SetTrigger("reborn");
        }
    }

    public void Reborn()
    {
        Instantiate(necromancer, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
