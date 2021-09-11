using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { normal, fire, water, wind, earth }

public class Ability : ScriptableObject
{
    // TODO: Defensive ability branch.
    // These values dont work for a defensive ability!

    [Header("Generic Info")]
    public new string abilityName;
    public int abilitySPCost;
    public float animLenght; // lenght for each animation
    protected bool playingAnim;
    public bool isDefensiveSkill;
    public Sprite affinitySprite;

    [Header("Attack Info")]
    public int attackDamage;
    public DamageType typeOfDamage;
    public bool isMultihit;

    [Header("Defensive Info")]
    public int healAmount;
    public float damageReductionMultiplier;



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
