using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallDead : MonoBehaviour
{
    internal static bool facingRight;
    public GameObject explosion;

    void Update()
    {
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void Explode()
    {
        Instantiate(explosion, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        Destroy(gameObject);
    }
}
