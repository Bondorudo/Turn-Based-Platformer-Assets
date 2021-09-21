using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : GrantAbilityManager
{
    protected override void Start()
    {
        base.Start();
        if (player.hasGainedDash)
            ChangeColor();
    }

    protected override void GrantAbility()
    {
        if (!player.hasGainedDash)
        {
            base.GrantAbility();

            player.hasGainedDash = true;

            SaveSystem.SavePlayer(player);

            FloatingText.Create(transform.position, name, 8, 500);
        }
    }
}
