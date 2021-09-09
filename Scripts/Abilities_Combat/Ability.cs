using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { normal, fire, water, wind, earth }

public class Ability : ScriptableObject
{
    public new string abilityName;
    public int attackDamage;
    public int abilitySPCost;

    protected bool playingAnim;

    public float animLenght; // lenght for each animation

    public DamageType typeOfDamage;

    public bool isMultihit;

    public virtual void Activate()
    {

    }

    protected virtual void Animation()
    {
        // Trigger animation here;
        playingAnim = true;
        CombatGameManager.instance.StartCoroutine(AnimDuration());
    }

    IEnumerator AnimDuration()
    {
        yield return new WaitForSeconds(animLenght);

        // Trigger animation stop event here;
        CombatGameManager.instance.EndAnimation();

        playingAnim = false;
    }
}
