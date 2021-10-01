using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperDash : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.playerData.hasGainedSuperDash)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        if (!player.playerData.hasGainedSuperDash)
        {
            base.GrantAbility();

            player.playerData.hasGainedSuperDash = true;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
