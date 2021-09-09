using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJumpAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/DoubleJumpAttack")]
public class PlayerAttackDoubleJump : AbilityPlayer
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.player.anim.SetTrigger("AttackDoubleJump");
    }
}
