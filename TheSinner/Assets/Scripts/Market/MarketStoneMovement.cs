using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketStoneMovement : MonoBehaviour
{
    float speed;
    float cd;
    float startCd;

    private void Start()
    {
        speed = .3f;
        startCd = Random.Range(.8f, 1f);
        cd = startCd;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (cd < 0)
        {
            speed *= -1;
            cd = startCd;
        }
        else
        {
            cd -= Time.deltaTime;
        }
    }
}
