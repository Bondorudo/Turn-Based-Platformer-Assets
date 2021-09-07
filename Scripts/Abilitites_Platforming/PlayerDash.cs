using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        base.GrantAbility();
        Player.isDashUnlocked = true;
    }
}
