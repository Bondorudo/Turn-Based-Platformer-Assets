using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats { neutral, strong, weak, absorb, reflect, nullify }

public class CombatUnits : MonoBehaviour
{
    public GameObject target;
    public Animator anim;
    protected AbilityHolder abilityHolder;
    protected HealthBarBehaviour healthBar;

    protected GameState gameState;

    // List order is = (Fire, Water, Wind, Earth)
    public List<Stats> damageAtrributes = new List<Stats>();

    public bool isPlayerDead;

    public int maxHealth;
    protected int currentHealth;
    protected int damage;
    protected DamageType damageType;


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

    protected virtual void ChangeHealth(float change)
    {
        currentHealth += (int)change; 
        healthBar.SetHealth(currentHealth, maxHealth);


        FloatingText.Create(transform.position, change.ToString(), 15, 30);

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

    protected virtual void DealDamage()
    {
        // Send message lähettää arrayn jossa on tiedot
        // Send messagessa lähetetään damage määrä sekä damage tyyppi.
        // Tieto lähetetään CheckStats funktioon jossa damage tyypin mukaan jatketaan oikeaa polkua pitkin, ja lasketaan mitä damage numerolle pitäisi tehdä
        // Jonka jälkeen uusi damage määrä lähetetään ChangeHealth funktioon.

        Damage dmg = new Damage
        {
            damageAmount = -damage,
            damageType = damageType,
        };

        target.SendMessage("CheckStats", dmg);
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



    public void CheckStats(Damage dmg)
    {
        int i = (int)dmg.damageType;

        Debug.Log("Damage Type = " + dmg.damageType + " | Target Damage Attribute = " + damageAtrributes[i]);
        FloatingText.Create(new Vector2(transform.position.x, transform.position.y + 2), damageAtrributes[i].ToString(), 15, 300);

        // now receives damage amount and damage type
        switch (damageAtrributes[i])
        {
            case Stats.neutral:
                Neutral(dmg.damageAmount);
                break;
            case Stats.strong:
                Strong(dmg.damageAmount);
                break;
            case Stats.weak:
                Weak(dmg.damageAmount);
                break;
            case Stats.absorb:
                Absorb(dmg.damageAmount);
                break;
            case Stats.reflect:
                Reflect(dmg.damageAmount);
                break;
            case Stats.nullify:
                Nullify(dmg.damageAmount);
                break;
        }
    }

    // What to do when you are Neutral to incoming damage
    private void Neutral(int damage)
    {
        ChangeHealth(damage);
    }

    // What to do when you are Strong to incoming damage
    private void Strong(int damage)
    {
        float newDamage = damage * 0.6f;
        ChangeHealth(newDamage);
    }

    // What to do when you are Weak to incoming damage
    private void Weak(int damage)
    {
        float newDamage = damage * 1.4f;
        ChangeHealth(newDamage);
    }

    // What to do when you Absorb incoming damage
    private void Absorb(int damage)
    {
        ChangeHealth(-damage);
    }

    // What to do when you Reflect incoming damage
    private void Reflect(int damage)
    {
        Debug.Log("Reflect Attack");
    }

    // What to do when you Nullify incoming damage
    private void Nullify(int damage)
    {
        Debug.Log("Nullify Attack");
    }
}
