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
    }

    protected override void Update()
    {
        base.Update();

        TargetEnemy();
        UseAbility();
    }

    public void SelectAbility(int id)
    {
        isAbilitySelected = true;
        abilityIndex = id;
        isMultihit = abilityHolder.ability[abilityIndex].isMultihit;
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


    public void ChangeActionCount()
    {
        actionCount--;
    }

    private void TargetEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (CombatGameManager.instance.gameState == GameState.PlayerTurn && isAbilitySelected)
        {
            
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

        if (CombatGameManager.instance.gameState == GameState.EnemyTurn)
        {
            RemoveTarget();
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
