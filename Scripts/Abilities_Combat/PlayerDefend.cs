using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefendAbility", menuName = "PlayerAbilities/PlayerDefense/Defend")]
public class PlayerDefend : AbilityPlayer
{
    public override void Activate()
    {
        base.Activate();

        Animation();
        CombatGameManager.instance.player.anim.SetTrigger("Defend");
    }
}
