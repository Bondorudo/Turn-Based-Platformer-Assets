using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharmState { Locked, Available, Equipped }


[CreateAssetMenu(fileName = "Charm", menuName = "PlayerAbilities/Charm")]
public class CharmObject : ScriptableObject
{
    // Main use
    [Header("Primary Usage")]
    public List<SkillStats> damageAtrributes = new List<SkillStats>();

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

[System.Serializable]
public class Charm
{
    public string name;
    public int extraHealth;
    public int extraSP;
    public int extraBaseDamage;

    public List<SkillStats> damageAtrributes = new List<SkillStats>();

    public Charm(CharmObject charm)
    {
        for (int i = 0; i < charm.damageAtrributes.Count; i++)
        {
            damageAtrributes.Add(charm.damageAtrributes[i]);
        }

        name = charm.name;
        extraHealth = charm.extraHealth;
        extraSP = charm.extraSP;
        extraBaseDamage = charm.extraBaseDamage;
    }
    public void SecondaryUse()
    {
        StaticGameData.playerMaxHealth += extraHealth;
        StaticGameData.playerMaxSP += extraSP;
        StaticGameData.playerBaseDamage += extraBaseDamage;
    }
}

[System.Serializable]
public class SkillStats
{
    public DamageType damageType;
    public Stats damageAttribute;
}


