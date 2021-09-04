using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnits : MonoBehaviour
{
    public GameObject target;

    [SerializeField] protected int maxHealth;
    protected int currentHealth;


    protected virtual void Start()
    {
        currentHealth = maxHealth;
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
        Destroy(gameObject);
    }

    protected virtual void EndAnim()
    {
        CombatGameManager.instance.EndAnimation();
    }
}
