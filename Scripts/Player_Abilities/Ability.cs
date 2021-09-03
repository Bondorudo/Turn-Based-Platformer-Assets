using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string abilityName;
    public int attackDamage;

    protected GameObject targetedEnemy;

    public virtual void Activate()
    {
        targetedEnemy = CombatGameManager.instance.targeteddEnemy;
        CombatGameManager.instance.player.ChangeActionCount();
        CombatGameManager.instance.gameState = GameState.PlayerAnimation;
    }
}
