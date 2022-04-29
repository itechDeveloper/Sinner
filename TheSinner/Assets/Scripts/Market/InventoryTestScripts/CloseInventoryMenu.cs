using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventoryMenu : MonoBehaviour
{
    int x;
    private void Update()
    {
        if(Time.time >= .1f && x == 0)
        {
            gameObject.SetActive(false);
            x++;
        }
    }
}
