using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperDash : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        base.GrantAbility();
        Player.isSuperDashUnlocked = true;
        FloatingText.Create(transform.position, name, 8, 500);
    }
}
