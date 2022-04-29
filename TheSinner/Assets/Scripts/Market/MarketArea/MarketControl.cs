using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketControl : MonoBehaviour
{
    public GameObject comp1;
    public GameObject comp2;
    public GameObject craftarea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            comp1.gameObject.SetActive(false);
            comp2.gameObject.SetActive(false);
            craftarea.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            comp1.gameObject.SetActive(true);
            comp2.gameObject.SetActive(true);
            craftarea.gameObject.SetActive(false);
        }
    }
}
