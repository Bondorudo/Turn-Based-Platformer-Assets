using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string abilityName;
    public int attackDamage;

    protected CombatPlayer player;

    protected GameObject targetedEnemy;

    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<CombatPlayer>();
    }

    public virtual void Active()
    {
        player.ChangeActionCount();
        targetedEnemy = player.targetedEnemy;
        CombatGameManager.instance.gameState = GameState.PlayerAnimation;
        targetedEnemy.SendMessage("ChangeHealth", -attackDamage);
        DamagePopup.Create(targetedEnemy.transform.position, attackDamage, false);
    }
}
