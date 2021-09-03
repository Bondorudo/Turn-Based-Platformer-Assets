using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/JumpAttack")]
public class JumpAttackAbility : Ability
{
    public override void Active()
    {
        base.Active();

        player.anim.SetTrigger("AttackJump");
        Debug.Log("Jump Attack: Deal " + attackDamage + " Damage to enemy");
    }
}
