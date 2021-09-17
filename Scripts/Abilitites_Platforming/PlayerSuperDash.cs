using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperDash : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        if (!gameData.hasGrantedSuperDash)
        {
            base.GrantAbility();

            gameData.isSuperDashUnlocked = true;
            gameData.hasGrantedSuperDash = true;

            GameDataManager.instance.WriteFile();

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
