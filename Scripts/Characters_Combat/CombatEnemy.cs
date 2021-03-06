using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyLocation { TestArea, City, Depths, InnerCity, Forest }

public class CombatEnemy : CombatUnits
{
    public int moneyToGive;
    public int baseDamage;

    public EnemyLocation enemyLocation;

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
        currentSP -= abilityHolder.ability[abilityIndex].abilitySPCost;
        damage = abilityHolder.ability[abilityIndex].attackDamage;
        damageType = abilityHolder.ability[abilityIndex].typeOfDamage;
    }
}
