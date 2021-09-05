using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NormalAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/NormalAttack")]
public class PlayerAttackNormal : AbilityPlayer
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.player.anim.SetTrigger("AttackNormal");
    }
}
