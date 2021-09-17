using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappleHook : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        if (!gameData.hasGrantedGrappleHook)
        {
            base.GrantAbility();

            gameData.isGrappleHookUnlocked = true;
            gameData.hasGrantedGrappleHook = true;

            GameDataManager.instance.WriteFile();

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
