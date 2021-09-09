using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnemy : CombatUnits
{
    public int moneyToGive;
    public int baseDamage;

    protected override void Start()
    {
        base.Start();

        target = GameObject.Find("Player");
        abilityHolder = transform.GetComponent<AbilityHolder>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SelectAbilityToUse()
    {
        // TODO: Make enemy A.I more complex and more diverse

        // Select random ability to use, Then call useAbility
        int count = abilityHolder.ability.Count;
        int randomAbilityId = Random.Range(0, count);
        UseAbility(randomAbilityId);
    }

    private void UseAbility(int abilityIndex)
    {
        abilityHolder.ability[abilityIndex].Activate();
        damage = abilityHolder.ability[abilityIndex].attackDamage;
        damageType = abilityHolder.ability[abilityIndex].typeOfDamage;
    }
}
