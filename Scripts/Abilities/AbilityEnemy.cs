using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemy : Ability
{
    public override void Activate()
    {
        base.Activate();

        CombatGameManager.instance.gameState = GameState.EnemyAnimation;
    }
}
