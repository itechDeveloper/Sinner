using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrap : MonoBehaviour
{
    public int spellCounter;
    float cd;
    public float startCd;
    public float cdBetween;

    public GameObject spellTrap;
    public Transform point;

    float dirX;
    float dirY;
    public int lenght;
    float angleBetween;
    public float angleDiff;

    bool activated;

    float pi2 = 6.283185307179586476924f;

    GameObject player;
    public float distance;

    private void Start()
    {
        angleBetween = angleDiff;
        cd = startCd;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < distance)
        {
            if (cd <= 0 && !activated)
            {
                StartCoroutine("FireTrap");
                activated = true;
            }
            else
            {
                cd -= Time.deltaTime;
            }
        }
    }

    IEnumerator FireTrap()
    {
        for (int i = 0; i < spellCounter; i++)
        {
            angleBetween += pi2 / spellCounter;
            dirX = Mathf.Cos(angleBetween) * lenght;
            dirY = Mathf.Sin(angleBetween) * lenght;
            Instantiate(spellTrap, point.position, Quaternion.identity);
            spellTrap.GetComponent<TrapSpellMovement>().directionNumberX = dirX;
            spellTrap.GetComponent<TrapSpellMovement>().directionNumberY = dirY; 

            yield return new WaitForSeconds(cdBetween);   
        }

        cd = startCd;
        activated = false;
    }
}
