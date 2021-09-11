using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemy : Ability
{
    protected Transform actingEnemyTrans;

    public override void Activate()
    {
        base.Activate();

        actingEnemyTrans = CombatGameManager.instance.GetActingEnemy();

        CombatGameManager.instance.gameState = GameState.EnemyAnimation;
    }
    /*
    private Transform GetActingEnemy()
    {
        Transform enemyTransform = CombatGameManager.actingEnemy.GetComponent<Transform>();

        return enemyTransform;
    }*/
}
