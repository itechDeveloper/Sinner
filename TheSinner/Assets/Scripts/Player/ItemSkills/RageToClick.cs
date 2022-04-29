using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageToClick : MonoBehaviour
{
    public int level;
    int i;

    private void Start()
    {
        i = transform.parent.GetComponent<LoadItemsToClick>().i;
    }

    public void click()
    {
        if (PlayerMovement.rageCd <= 0)
        {
            PlayerMovement.canBeDamaged = false;

            //Skill cd
            if (level == 3)
            {
                PlayerMovement.rageCd = 8f;
                PlayerMovement.rageLastCd = 12f;
            }else if(level == 2)
            {
                PlayerMovement.rageCd = 11f;
                PlayerMovement.rageLastCd = 9f;
            }
            else if(level == 1)
            {
                PlayerMovement.rageCd = 14f;
                PlayerMovement.rageLastCd = 6f;
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
