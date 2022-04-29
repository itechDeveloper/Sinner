using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoves : MonoBehaviour
{
    public float speed = 2;
    public GameObject target;
   
    void Update()
   
    {
       
        
        float step = speed * Time.deltaTime;

       
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);


        

    }

    
}
