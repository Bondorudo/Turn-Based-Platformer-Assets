using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats { neutral, strong, weak, absorb, reflect, nullify }

public class CombatUnits : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    public Animator anim;
    protected AbilityHolder abilityHolder;
    protected HealthBarBehaviour healthBar;

    protected GameState gameState;

    // List order is = (Normal, Fire, Water, Wind, Earth)
    public List<Stats> damageAtrributes = new List<Stats>();

    public bool isPlayerDead;
    protected bool isMultihit;
    protected bool isDefensiveSkill;
    public bool defendFromAttacks;

    public int maxHealth;
    protected int currentHealth;
    public float defenceValue = 1;
    public int maxSP;
    protected int currentSP;
    protected int damage;
    protected int healAmount;
    protected DamageType damageType;


    protected virtual void Start()
    {
        healthBar = GetComponentInChildren<HealthBarBehaviour>();
        isPlayerDead = false;
        currentHealth = maxHealth;
        currentSP = maxSP;

        healthBar.SetHealth(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth, maxHealth);

        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        abilityHolder = transform.GetComponent<AbilityHolder>();
    }

    protected virtual void HealDamage(float change)
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

    protected virtual void TakeDamage(float damage)
    {
        int damageToTake = (int)(damage * -defenceValue);
        currentHealth -= damageToTake;

        healthBar.SetHealth(currentHealth, maxHealth);

        FloatingText.Create(transform.position, damageToTake.ToString(), 15, 30);

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
        // Send message l�hett�� arrayn jossa on tiedot
        // Send messagessa l�hetet��n damage m��r� sek� damage tyyppi.
        // Tieto l�hetet��n CheckStats funktioon jossa damage tyypin mukaan jatketaan oikeaa polkua pitkin, ja lasketaan mit� damage numerolle pit�isi tehd�
        // Jonka j�lkeen uusi damage m��r� l�hetet��n ChangeHealth funktioon.

        Damage dmg = new Damage
        {
            damageAmount = -damage,
            damageType = damageType,
        };

        
        if (targets.Count >= 1)
        {
            foreach (GameObject trg in targets)
            {
                trg.SendMessage("CheckStats", dmg);
            }
            targets.Clear();
        }

        if (target != null)
            target.SendMessage("CheckStats", dmg);
    }

    protected virtual void HealFromDamage()
    {
        if (target != null)
            target.SendMessage("HealDamage", healAmount);
    }

    protected virtual void DefendFromDamage()
    {
        // Take less damage From the NEXT attack and only that so reset after taking damage
        // Create defence values for units

        
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
        TakeDamage(damage);
    }

    // What to do when you are Strong to incoming damage
    private void Strong(int damage)
    {
        float newDamage = damage * 0.6f;
        TakeDamage(newDamage);
    }

    // What to do when you are Weak to incoming damage
    private void Weak(int damage)
    {
        float newDamage = damage * 1.4f;
        TakeDamage(newDamage);
    }

    // What to do when you Absorb incoming damage
    private void Absorb(int damage)
    {
        TakeDamage(-damage);
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
