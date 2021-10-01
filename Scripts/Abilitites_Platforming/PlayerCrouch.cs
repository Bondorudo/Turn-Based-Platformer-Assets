using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.playerData.hasGainedCrouch)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        if (!player.playerData.hasGainedCrouch)
        {
            base.GrantAbility();

            player.playerData.hasGainedCrouch = true;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
