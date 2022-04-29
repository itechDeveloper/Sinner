using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritToClick : MonoBehaviour
{
    public int level;
    int i;

    private void Start()
    {
        i = transform.parent.GetComponent<LoadItemsToClick>().i;
    }

    public void click()
    {
        PlayerMovement.canBeDamaged = false;
        PlayerMovement.canDestroySpirit = false;

        if (PlayerMovement.spiritCd <= 0)
        {
            //Skill cd
            if (level == 3)
            {
                PlayerMovement.spiritCd = 8f;
                PlayerMovement.spiritLastCd = 12f;
                PlayerMovement.spiritLife = 100;
            }
            else if (level == 2)
            {
                PlayerMovement.spiritCd = 11f;
                PlayerMovement.spiritLastCd = 9f;
                PlayerMovement.spiritLife = 70;
            }
            else if (level == 1)
            {
                PlayerMovement.spiritCd = 14f;
                PlayerMovement.spiritLastCd = 6f;
                PlayerMovement.spiritLife = 40;
            }

            //Ability cd apperance
            if (i == 0)
            {
                AbilityCooldowns.cooldown1 = 20;
                AbilityCooldowns.canActivateAbility1 = true;
            }
            else if (i == 1)
            {
                AbilityCooldowns.cooldown2 = 20;
                AbilityCooldowns.canActivateAbility2 = true;
            }
            else if (i == 2)
            {
                AbilityCooldowns.cooldown3 = 20;
                AbilityCooldowns.canActivateAbility3 = true;
            }
        }
    }
}
