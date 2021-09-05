using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/DashAttack")]
public class PlayerAttackDash : AbilityPlayer
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.player.anim.SetTrigger("AttackDash");
    }
}
