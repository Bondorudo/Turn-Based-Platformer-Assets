using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.hasGainedCrouch)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        if (!player.hasGainedCrouch)
        {
            base.GrantAbility();

            player.hasGainedCrouch = true;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
