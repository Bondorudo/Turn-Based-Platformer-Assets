using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : ScriptableObject
{
    // Main use
    public List<Stats> damageAtrributes = new List<Stats>();

    // Secondary use
    public int extraHealth;
    public int extraSP;
    public int extraBaseDamage;
    // other possible stats


    public virtual void SecondaryUse()
    {

    }
}
