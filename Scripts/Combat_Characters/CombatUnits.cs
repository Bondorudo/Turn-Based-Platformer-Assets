using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnits : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void DoAction()
    {
        Debug.Log("No Action Logic Implemented");
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
        Debug.Log(gameObject.name + " has Died");
        Destroy(gameObject);
    }

    protected virtual void EndAnim()
    {
        CombatGameManager.instance.EndAnimation();
    }
}
