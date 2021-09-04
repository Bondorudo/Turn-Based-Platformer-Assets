using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnits : MonoBehaviour
{
    public GameObject target;
    public Animator anim;
    protected AbilityHolder abilityHolder;

    protected GameState gameState;

    public int maxHealth;
    protected int currentHealth;
    protected int damage;


    protected virtual void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        abilityHolder = transform.GetComponent<AbilityHolder>();
    }

    protected virtual void ChangeHealth(int change)
    {
        currentHealth += change;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    protected virtual void Death()
    {
        if (gameObject.tag == "Enemy")
        {
            CombatGameManager.instance.listOfCurrentEnemies.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    protected virtual void EndAnim()
    {
        CombatGameManager.instance.EndAnimation();
    }
}
