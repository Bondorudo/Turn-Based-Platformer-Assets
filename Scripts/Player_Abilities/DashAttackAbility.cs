using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/DashAttack")]
public class DashAttackAbility : Ability
{
    public override void Active()
    {
        base.Active();

        player.anim.SetTrigger("AttackDash");
        Debug.Log("Dash Attack: Deal " + attackDamage + " Damage to enemy");
    }
}
