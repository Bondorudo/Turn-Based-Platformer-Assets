using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.playerData.hasGainedDoubleJump)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        base.GrantAbility();
        if (!player.playerData.hasGainedDoubleJump)
        {
            player.playerData.hasGainedDoubleJump = true;
            player.playerData.maxJumps = 2;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
            ChangeColor();
        }
    }
}
