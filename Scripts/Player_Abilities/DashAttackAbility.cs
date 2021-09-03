using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/DashAttack")]
public class DashAttackAbility : Ability
{
    public override void Activate()
    {
        base.Activate();

        CombatGameManager.instance.player.anim.SetTrigger("AttackDash");
    }
}
