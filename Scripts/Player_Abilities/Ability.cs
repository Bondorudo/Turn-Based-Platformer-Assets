using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string abilityName;
    public int attackDamage;

    private GameState gameState;

    private CombatPlayer player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<CombatPlayer>();
    }

    public virtual void Active(GameObject parent)
    {
        player.ChangeActionCount();
        gameState = GameState.PlayerAnimation;
    }
}
