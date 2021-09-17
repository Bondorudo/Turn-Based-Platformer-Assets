using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : GrantAbilityManager
{
    protected override void GrantAbility()
    {
        base.GrantAbility();

        player.maxJumpAmount = 2;
        player.SavePlayer();

        FloatingText.Create(transform.position, name, 8, 500);
    }
}
