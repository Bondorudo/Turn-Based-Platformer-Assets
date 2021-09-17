using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        if (!gameData.hasGrantedCrouch)
        {
            base.GrantAbility();

            gameData.isCrouchUnlocked = true;
            gameData.hasGrantedCrouch = true;

            GameDataManager.instance.WriteFile();

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
