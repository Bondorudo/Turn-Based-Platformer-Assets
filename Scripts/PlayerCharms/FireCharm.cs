using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireCharm", menuName = "PlayerAbilities/PlayerCharms/FireCharm")]
public class FireCharm : Charm
{
    public override void SecondaryUse()
    {
        base.SecondaryUse();

        StaticGameData.playerMaxHealth += extraHealth;
    }
}
