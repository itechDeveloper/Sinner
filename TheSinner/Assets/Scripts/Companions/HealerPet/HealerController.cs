using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{
    GameObject player;
    public int heal;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    public void Heal()
    {
        player.GetComponent<PlayerMovement>().GetHeal(heal);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
