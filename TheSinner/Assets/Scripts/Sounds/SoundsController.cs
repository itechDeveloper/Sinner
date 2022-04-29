using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsController : MonoBehaviour
{

    
    
    public void sesac()
    {
        AudioListener.volume = 1;
    }

    public void seskapat()
    {
        AudioListener.volume = 0;
    }


}
