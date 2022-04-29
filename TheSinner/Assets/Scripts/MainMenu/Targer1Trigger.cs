using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targer1Trigger : MonoBehaviour
{
    public GameObject options;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Button"))
        {
            options.gameObject.SetActive(true);

        }
        
    }
}

