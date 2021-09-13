using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "PlayerAbilities/Charm")]

public class Charm : ScriptableObject
{
    // Main use
    [Header("Primary Usage")]
    public List<Stats> damageAtrributes = new List<Stats>();

    // Secondary use
    [Header("Secondary Usage")]
    public int extraHealth;
    public int extraSP;
    public int extraBaseDamage;

    // other possible stats
    [Header("Other Info about charm")]
    public Sprite charmSprite;
    public CharmState charmState;
    // TODO: Set Charm State to unlocked when picking it up

    public void SecondaryUse()
    {
        StaticGameData.playerMaxHealth += extraHealth;
        StaticGameData.playerMaxSP += extraSP;
        StaticGameData.playerBaseDamage += extraBaseDamage;
    }

    public void UnlockCharm()
    {
        charmState = CharmState.Available;
    }

    public void EquipCharm()
    {
        charmState = CharmState.Equipped;
    }

    public void AvailableCharm()
    {
        charmState = CharmState.Available;
    }
}
