using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMarket : MonoBehaviour
{
    public GameObject item3infos;

    // Start is called before the first frame update

    public void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            item3infos.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            item3infos.gameObject.SetActive(false);
        }
    }
}