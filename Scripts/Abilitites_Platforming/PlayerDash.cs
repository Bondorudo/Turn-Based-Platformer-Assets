using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        if (!gameData.hasGrantedDash)
        {
            base.GrantAbility();

            gameData.isDashUnlocked = true;
            gameData.hasGrantedDash = true;

            GameDataManager.instance.WriteFile();

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
