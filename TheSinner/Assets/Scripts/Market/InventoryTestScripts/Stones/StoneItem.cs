using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneItem : MonoBehaviour
{
    public GameObject itemButton;
    public GameObject info1;

    public void Use()
    {
        info1.gameObject.SetActive(true);
    }
}
