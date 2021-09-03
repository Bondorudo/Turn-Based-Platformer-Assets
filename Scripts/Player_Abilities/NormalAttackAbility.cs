using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NormalAttackAbility", menuName = "PlayerAbilities/PlayerAttacks/NormalAttack")]
public class NormalAttackAbility : Ability
{
    public override void Active(GameObject parent)
    {
        base.Active(parent);

        Debug.Log("Normal Attack: Deal " + attackDamage + " Damage to enemy");
    }
}
