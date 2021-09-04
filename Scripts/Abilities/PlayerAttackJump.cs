using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/JumpAttack")]
public class PlayerAttackJump : AbilityPlayer
{
    public override void Activate()
    {
        base.Activate();

        CombatGameManager.instance.player.anim.SetTrigger("AttackJump");
    }
}
