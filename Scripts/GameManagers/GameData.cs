using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // MAKE THIS STATIC??????

    // Variables in the game to save.

    public int moneyHeld;

    //public List<Charm> charmsEquipped;
    //public List<Charm> charmsHeld;

    public bool isCrouchUnlocked;
    public bool hasGrantedCrouch;

    public int maxJumpings = 1;
    public bool isDoubleJumpUnlocked;
    public bool hasGrantedDoubleJump;

    public bool isDashUnlocked;
    public bool hasGrantedDash;

    public bool isSuperDashUnlocked;
    public bool hasGrantedSuperDash;

    public bool isGrappleHookUnlocked;
    public bool hasGrantedGrappleHook;
}
