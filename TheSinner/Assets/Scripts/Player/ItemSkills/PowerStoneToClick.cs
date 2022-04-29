using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStoneToClick : MonoBehaviour
{
    public int level;
    int i;

    private void Start()
    {
        i = transform.parent.GetComponent<LoadItemsToClick>().i;
    }

    public void click()
    {
        if (PlayerMovement.powerCd <= 0)
        {
            //Skill cd
            if (level == 3)
            {
                PlayerMovement.powerCd = 8f;
                PlayerMovement.powerLastCd = 12f;    
            }
            else if (level == 2)
            {
                PlayerMovement.powerCd = 11f;
                PlayerMovement.powerLastCd = 9f;     
            }
            else
            {
                PlayerMovement.powerCd = 14f;
                PlayerMovement.powerLastCd = 6f;
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
