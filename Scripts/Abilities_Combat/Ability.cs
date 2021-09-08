using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { fire, water, wind, earth }

public class Ability : ScriptableObject
{
    public new string abilityName;
    public int attackDamage;

    protected bool playingAnim;

    public float animLenght; // lenght for each animation

    public DamageType typeOfDamage;

    public virtual void Activate()
    {

    }

    protected virtual void Animation()
    {
        playingAnim = true;

        // Trigger animation here;

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
