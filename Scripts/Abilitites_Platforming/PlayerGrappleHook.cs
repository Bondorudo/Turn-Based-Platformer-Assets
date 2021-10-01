using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappleHook : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.playerData.hasGainedGrappleHook)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        if (!player.playerData.hasGainedGrappleHook)
        {
            base.GrantAbility();

            player.playerData.hasGainedGrappleHook = true;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
