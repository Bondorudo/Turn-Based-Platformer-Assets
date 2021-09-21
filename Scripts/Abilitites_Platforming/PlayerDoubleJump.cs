using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.hasGainedDoubleJump)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        base.GrantAbility();
        if (!player.hasGainedDoubleJump)
        {
            player.hasGainedDoubleJump = true;
            player.maxJumpAmount = 2;
            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
        else
            ChangeColor();
    }
}
