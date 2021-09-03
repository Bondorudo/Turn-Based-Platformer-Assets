using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnemy : CombatUnits
{
    public int level;
    public int xpToGive;
    public int baseDamage;


    public void Action()
    {
        DoAction();
    }

    protected override void DoAction()
    {
        NormalAttack();
    }

    private void NormalAttack()
    {
        Debug.Log("Normal Attack Action");
    }

    private void Defend()
    {
        Debug.Log("Defend Action");
    }
}
