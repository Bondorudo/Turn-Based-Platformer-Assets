using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnits : MonoBehaviour
{
    public GameObject target;
    public Animator anim;
    protected AbilityHolder abilityHolder;
    protected HealthBarBehaviour healthBar;

    protected GameState gameState;

    public bool isPlayerDead;

    public int maxHealth;
    protected int currentHealth;
    protected int damage;


    protected virtual void Start()
    {
        healthBar = GetComponentInChildren<HealthBarBehaviour>();
        isPlayerDead = false;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        abilityHolder = transform.GetComponent<AbilityHolder>();
    }

    protected virtual void ChangeHealth(int change)
    {
        currentHealth += change; 
        healthBar.SetHealth(currentHealth, maxHealth);

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
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Player")
        {
            isPlayerDead = true;
        }
    }

    protected virtual void EndAnim()
    {
        CombatGameManager.instance.EndAnimation();
    }
}
