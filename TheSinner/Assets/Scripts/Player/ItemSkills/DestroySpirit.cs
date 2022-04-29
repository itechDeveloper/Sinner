using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpirit : MonoBehaviour
{
    void Update()
    {
        if (PlayerMovement.canDestroySpirit)
        {
            PlayerMovement.canDestroySpirit = false;
            Destroy(gameObject);
        }
    }
}
