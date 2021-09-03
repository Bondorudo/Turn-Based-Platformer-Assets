using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NormalAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/NormalAttack")]
public class NormalAttackAbility : Ability
{
    public override void Activate()
    {
        base.Activate();

        CombatGameManager.instance.player.anim.SetTrigger("AttackNormal");
    }
}
