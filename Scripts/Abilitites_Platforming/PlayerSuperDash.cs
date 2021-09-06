using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperDash : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        Player.isSuperDashUnlocked = true;
    }
}
