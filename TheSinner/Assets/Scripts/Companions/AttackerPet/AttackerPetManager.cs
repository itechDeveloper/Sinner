using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerPetManager : MonoBehaviour
{
    public GameObject attackerPet;
    public Transform target;

    public float coolDown;
    float coolDownTimer;
    Vector2 whereToSpawn;
    internal static bool isDead;

    private void Start()
    {
        isDead = true;
        target = GameObject.FindGameObjectWithTag("petPos").transform;
        coolDownTimer = coolDown;
    }

    void Update()
    {
        if (isDead)
        {
            Spawn();   
        }
    }

    void Spawn()
    {
        if (coolDownTimer <= 0)
        {
            FindSpawningPlace();
            Instantiate(attackerPet, whereToSpawn, Quaternion.identity);
            coolDownTimer = coolDown;
            isDead = false;
        }
        else
        {
            coolDownTimer -= Time.deltaTime;
        }
    }

    void FindSpawningPlace()
    {
        whereToSpawn = new Vector2(target.transform.position.x, target.transform.position.y);
    }
}
