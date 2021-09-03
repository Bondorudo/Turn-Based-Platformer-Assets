using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/JumpAttack")]
public class JumpAttackAbility : Ability
{
    public override void Active(GameObject parent)
    {
        base.Active(parent);

        Debug.Log("Jump Attack: Deal " + attackDamage + " Damage to enemy");
    }
}
