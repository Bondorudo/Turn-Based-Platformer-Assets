using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NormalAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/NormalAttack")]
public class NormalAttackAbility : Ability
{
    public override void Active()
    {
        base.Active();

        player.anim.SetTrigger("AttackNormal");
        Debug.Log("Normal Attack: Deal " + attackDamage + " Damage to enemy");
    }
}
