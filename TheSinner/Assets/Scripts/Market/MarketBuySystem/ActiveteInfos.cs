using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveteInfos : MonoBehaviour
{
    public GameObject infos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infos.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infos.gameObject.SetActive(false);
        }
    }
}
