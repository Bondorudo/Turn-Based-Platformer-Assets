using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperDash : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.hasGainedSuperDash)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        if (!player.hasGainedSuperDash)
        {
            base.GrantAbility();

            player.hasGainedSuperDash = true;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
