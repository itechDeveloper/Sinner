using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSummon : MonoBehaviour
{
    public GameObject wolf;
    public GameObject summonPoint;
    public void Destroy()
    {
        Instantiate(wolf, transform.position, Quaternion.identity);
        Destroy(summonPoint);
    }
}
