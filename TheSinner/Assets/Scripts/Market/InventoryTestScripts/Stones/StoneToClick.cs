using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneToClick : MonoBehaviour
{
    public void click() 
    {
        PlayerMovement.powerCd = 10f;
        PlayerMovement.powerLastCd = 10f;
    }
}
