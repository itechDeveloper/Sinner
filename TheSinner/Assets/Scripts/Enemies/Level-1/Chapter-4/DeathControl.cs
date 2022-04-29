using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
    public static bool grandmaDead, redHoodDead;

    public GameObject nextchapter, portal;
    void Update()
    {
        if (grandmaDead && redHoodDead)
        {
            nextchapter.SetActive(true);
            portal.SetActive(true);
        }
    }
}
