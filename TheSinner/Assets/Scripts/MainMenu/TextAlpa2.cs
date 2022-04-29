using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextAlpa2 : MonoBehaviour
{
    public Text gamename;
    public string newstring;
    public Color newcolor;
    public bool ters;

    public void Start()
    {
        gamename.text = newstring;
        newstring = "SINNER";
        gamename.color = newcolor;
    }

    public void Update()
    {
        gamename.color = newcolor;
        

        if (ters == false)
        {

            newcolor.a += 0.006f;
            if(newcolor.a >= 1f)
            {
                ters = true;
            }
        }
        else if(newcolor.a > 0f)
        {
            newcolor.a -= 0.006f;
        }
    }
}
