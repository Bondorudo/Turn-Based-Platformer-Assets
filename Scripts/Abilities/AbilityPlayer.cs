using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPlayer : Ability
{
    public override void Activate()
    {
        base.Activate();

        CombatGameManager.instance.player.ChangeActionCount();
        CombatGameManager.instance.gameState = GameState.PlayerAnimation;
    }
}
