using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDead : MonoBehaviour
{
    internal static bool facingRight;
    float countDown;

    private void Start()
    {
        countDown = 5f;
    }

    void Update()
    {
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            Destroy(gameObject);
        }
    }
}
