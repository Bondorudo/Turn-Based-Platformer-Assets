using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalAttackAbility", menuName = "EnemyAbilities/EnemyAttacks/NormalAttack")]
public class EnemyAttackNormal : AbilityEnemy
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.actingEnemy.anim.SetTrigger("EnemyAttackNormal");
    }
}
