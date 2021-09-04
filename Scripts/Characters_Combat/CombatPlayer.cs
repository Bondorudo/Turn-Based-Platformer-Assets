using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : CombatUnits
{
    private GameObject targetIndicator;

    private bool isAbilitySelected;

    public int actionCount;
    public int baseActionCount = 1;
    private int abilityIndex = 0;


    protected override void Start()
    {
        base.Start();
        actionCount = baseActionCount;
        isAbilitySelected = false;
    }

    protected override void Update()
    {
        base.Update();

        TargetEnemy();
        UseAbility();
    }

    // use abilites -> target -> use abilities -> space bar

    public void SelectAbility(int id)
    {
        isAbilitySelected = true;
        abilityIndex = id;
    }

    public void UseAbility()
    {
        if (target != null)
        {
            if (Input.GetKeyDown("space"))
            {
                abilityHolder.ability[abilityIndex].Activate();
                damage = abilityHolder.ability[abilityIndex].attackDamage;
                isAbilitySelected = false;
            }
        }
    }

    public void DealDamage()
    {
        target.SendMessage("ChangeHealth", -damage);
        DamagePopup.Create(target.transform.position, damage, 15);
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
            if (Input.GetMouseButton(0))
            {
                if (!hit)
                    return;

                if (hit.transform.tag == "Enemy")
                {
                    target = hit.transform.gameObject;
                    targetIndicator = CombatGameManager.instance.targetIndicator;
                    targetIndicator.transform.position = hit.transform.position;
                    targetIndicator.SetActive(true);
                }
            }
        }

        if (CombatGameManager.instance.gameState == GameState.EnemyTurn)
        {
            RemoveTarget();
        }
    }

    public void RemoveTarget()
    {
        targetIndicator.SetActive(false);
        target = null;
    }
}
