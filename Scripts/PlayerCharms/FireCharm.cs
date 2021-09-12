using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCharm : Charm
{
    public override void SecondaryUse()
    {
        base.SecondaryUse();

        StaticGameData.playerMaxHealth += extraHealth;
    }
}
