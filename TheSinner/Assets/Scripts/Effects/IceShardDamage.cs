using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardDamage : MonoBehaviour
{
    public int damage;
    public GameObject endIceShard;

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, -90f);
        if (transform.position.y <= -2.6f)
        {
            Instantiate(endIceShard, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Instantiate(endIceShard, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
