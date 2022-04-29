using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIceShard : MonoBehaviour
{
    public GameObject iceShard;

    private void Update()
    {
        transform.eulerAngles = new Vector3(0,0,-90);
    }
    public void Instantiate()
    {
        Instantiate(iceShard, transform.position, Quaternion.identity);
    }
}
