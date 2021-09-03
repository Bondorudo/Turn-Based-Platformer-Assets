using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLibrary : MonoBehaviour
{
    public static PlayerAttackLibrary instance;
    private void Awake()
    {
        instance = this;
    }

    // Every movement tool gained in platforming can also be used as an attack in combat
    // Different movement tools can be combined to create more powerful atttacks


    public void NormalPhysicalAttack(int baseDamage)
    {
        // Deal damage
        // Play attack animation
    }

    public void DashAttack()
    {

    }

    public void SuperDashAttack()
    {

    }

    public void GroundPoundAttack()
    {

    }
}
