using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        base.GrantAbility();
        Player.baseJumpAmount = 2;
        FloatingText.Create(transform.position, name, 8, 500);
    }
}
