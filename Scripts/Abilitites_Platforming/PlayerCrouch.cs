using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        Player.isCrouchUnlocked = true;
    }
}