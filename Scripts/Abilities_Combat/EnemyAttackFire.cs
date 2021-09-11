using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAttackAbility", menuName = "EnemyAbilities/EnemyAttacks/FireAttack")]
public class EnemyAttackFire : AbilityEnemy
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.actingEnemy.anim.SetTrigger("EnemyAttackFire");
        
        FloatingText.Create(actingEnemyTrans.position, "Fire Attack", 10, 800);
    }
}
