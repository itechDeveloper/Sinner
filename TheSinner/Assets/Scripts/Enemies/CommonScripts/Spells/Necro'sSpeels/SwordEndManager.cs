using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEndManager : MonoBehaviour
{
    public float lifeTime;
    public int damage;
    public GameObject destroyAnim;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Instantiate(destroyAnim, new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Instantiate(destroyAnim, new Vector2(transform.position.x, transform.position.y -1f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
