using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerMovement playerr;
    public Image fillimage;
    private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(slider.value <= slider.minValue)
        //{
        //    fillimage.enabled = false;
        //}
        float fillValue = playerr.currentHealth / playerr.maxHealth;
        slider.value = fillValue;
    }
}
