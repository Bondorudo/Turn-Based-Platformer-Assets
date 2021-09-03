using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/JumpAttack")]
public class JumpAttackAbility : Ability
{
    public override void Activate()
    {
        base.Activate();

        CombatGameManager.instance.player.anim.SetTrigger("AttackJump");
    }
}
