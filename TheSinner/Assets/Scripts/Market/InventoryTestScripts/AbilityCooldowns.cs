using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldowns : MonoBehaviour
{
    public Image abilityImage1;
    public static float cooldown1;
    public static bool isCooldown1;
    public static bool canActivateAbility1;

    public Image abilityImage2;
    public static float cooldown2;
    public static bool isCooldown2;
    public static bool canActivateAbility2;

    public Image abilityImage3;
    public static float cooldown3;
    public static bool isCooldown3;
    public static bool canActivateAbility3;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
    }

    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
    }

    void Ability1()
    {
        if (!isCooldown1 && canActivateAbility1)
        {
            isCooldown1 = true;
            canActivateAbility1 = false;
            abilityImage1.fillAmount = 1;
        }

        abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

        if (abilityImage1.fillAmount <= 0)
        {
            abilityImage1.fillAmount = 0;
            isCooldown1 = false;
        }
    }

    void Ability2()
    {
        if (!isCooldown2 && canActivateAbility2)
        {
            isCooldown2 = true;
            canActivateAbility2 = false;
            abilityImage2.fillAmount = 1;
        }

        abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

        if (abilityImage2.fillAmount <= 0)
        {
            abilityImage2.fillAmount = 0;
            isCooldown2 = false;
        }
    }

    void Ability3()
    {
        if (!isCooldown3 && canActivateAbility3)
        {
            isCooldown3 = true;
            canActivateAbility1 = false;
            abilityImage3.fillAmount = 1;
        }

        abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

        if (abilityImage3.fillAmount <= 0)
        {
            abilityImage3.fillAmount = 0;
            isCooldown3 = false;
        }
    }
}
