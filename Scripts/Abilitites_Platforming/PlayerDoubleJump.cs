using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        Player.baseJumpAmount = 2;
    }
}
