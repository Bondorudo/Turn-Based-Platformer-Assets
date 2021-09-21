using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxJumps;
    public int charm;



    public bool hasGainedDoubleJump;
    public bool hasGainedCrouch;
    public bool hasGainedDash;
    public bool hasGainedSuperDash;
    public bool hasGainedGrappleHook;



    public PlayerData (Player player)
    {
        /*
        maxJumps = player.maxJumpAmount;
        hasGainedDoubleJump = player.hasGainedDoubleJump;
        */
    }
}
