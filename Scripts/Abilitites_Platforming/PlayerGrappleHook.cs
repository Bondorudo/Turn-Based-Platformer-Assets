using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappleHook : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        base.GrantAbility();
        Player.isGrappleUnlocked = true;
    }
}
