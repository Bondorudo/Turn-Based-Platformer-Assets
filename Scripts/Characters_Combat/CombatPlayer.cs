using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : CombatUnits
{
    private SPBarBehaviour spBar;

    private List<GameObject> targetIndicators = new List<GameObject>();

    private bool isAbilitySelected;

    public int actionCount;
    public int baseActionCount = 1;
    private int abilityIndex = 0;


    protected override void Start()
    {

        base.Start();

        spBar = GetComponentInChildren<SPBarBehaviour>();

        spBar.SetSP(currentSP, maxSP);
        spBar.SetSP(currentSP, maxSP);

        actionCount = baseActionCount;
        isAbilitySelected = false;

        TargetIndicatorPool();
        RemoveTarget();

        SetCharms();

        currentHealth = maxHealth;
    }

    protected override void Update()
    {
        base.Update();

        anim.SetBool("IsDefending", defendFromAttacks);


        Targeting();
        UseAbility();
    }

    private void SetCharms()
    {
        damageAtrributes = StaticGameData.playerDamageAttributes;
        maxHealth = StaticGameData.playerMaxHealth;
    }

    public void SelectAbility(int id)
    {
        isAbilitySelected = true;
        abilityIndex = id;
        isMultihit = abilityHolder.ability[abilityIndex].isMultihit;
        isDefensiveSkill = abilityHolder.ability[abilityIndex].isDefensiveSkill;
        RemoveTarget();
    }

    public void UseAbility()
    {
        if (targets.Count >= 1 || target != null)
        {
            if (Input.GetKeyDown("space"))
            {
                if (currentSP >= abilityHolder.ability[abilityIndex].abilitySPCost)
                {
                    abilityHolder.ability[abilityIndex].Activate();
                    currentSP -= abilityHolder.ability[abilityIndex].abilitySPCost;
                    damage = abilityHolder.ability[abilityIndex].attackDamage;
                    damageType = abilityHolder.ability[abilityIndex].typeOfDamage;
                    healAmount = abilityHolder.ability[abilityIndex].healAmount;
                    defenceValue = abilityHolder.ability[abilityIndex].defenceValue;
                    isAbilitySelected = false; 
                    spBar.SetSP(currentSP, maxSP);
                }
                else
                {
                    FloatingText.Create(transform.position, "Not Enough SP", 10, 1000);
                }
            }
        }
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        RemoveTarget();
    }

    protected override void HealFromDamage()
    {
        base.HealFromDamage();
        RemoveTarget();
    }

    protected override void DefendFromDamage()
    {
        base.DefendFromDamage();
        RemoveTarget();
    }


    public void ChangeActionCount()
    {
        actionCount--;
    }

    private void Targeting()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (gameState == GameState.PlayerTurn)
        {
            if (isAbilitySelected)
            {
                if (!isDefensiveSkill)
                {
                    // Skill is made for attacking enemies | Target an enemy
                    if (isMultihit)
                    {
                        // Select all enemies
                        for (int i = 0; i < CombatGameManager.instance.listOfCurrentEnemies.Count; i++)
                        {
                            int id = i;
                            if (!targets.Contains(CombatGameManager.instance.listOfCurrentEnemies[id]))
                                targets.Add(CombatGameManager.instance.listOfCurrentEnemies[id]);
                            targetIndicators[id].transform.position = targets[id].transform.position;
                            targetIndicators[id].SetActive(true);
                        }
                    }

                    else if (!isMultihit && Input.GetMouseButton(0))
                    {
                        if (!hit)
                            return;

                        if (hit.transform.tag == "Enemy")
                        {
                            target = hit.transform.gameObject;
                            targetIndicators[0].transform.position = hit.transform.position;
                            targetIndicators[0].SetActive(true);
                        }
                    }
                }
                else
                {
                    // Skill is made for defending | Target self
                    target = transform.gameObject;
                    targetIndicators[0].transform.position = transform.position;
                    targetIndicators[0].SetActive(true);
                }
            }
        }
    }

    private void TargetIndicatorPool()
    {
        while (targetIndicators.Count <= 5)
        {
            targetIndicators.Add(Instantiate(CombatGameManager.instance.targetIndicator));
        }
    }

    public void RemoveTarget()
    {
        target = null;

        foreach (GameObject indicator in targetIndicators)
        {
            indicator.SetActive(false);
        }
    }
}
