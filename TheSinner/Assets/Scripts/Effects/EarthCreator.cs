using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCreator : MonoBehaviour
{
    public GameObject rock;
    float timer;

    void Start()
    {
        timer = .5f;
    }

    
    void Update()
    {

        if (!MonkManManager.earthActive || MonkManManager.monkDead)
        {
            Destroy(gameObject);
        }

        if (timer <= 0)
        {
            Instantiate(rock, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            if (MonkManManager.activateUltimate)
            {
                timer = 1.75f;
            }
            else
            {
                timer = .75f;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
