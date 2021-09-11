using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAbility", menuName = "PlayerAbilities/PlayerDefense/Heal")]
public class PlayerHeal : AbilityPlayer
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.player.anim.SetTrigger("Heal");
    }
}
