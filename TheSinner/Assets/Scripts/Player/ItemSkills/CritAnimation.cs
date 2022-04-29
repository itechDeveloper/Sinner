using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritAnimation : MonoBehaviour
{    
    void Start()
    {
        if (PlayerMovement.facingRight)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
