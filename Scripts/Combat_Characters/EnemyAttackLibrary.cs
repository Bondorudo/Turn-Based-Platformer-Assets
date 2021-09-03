using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackLibrary : MonoBehaviour
{
    public static EnemyAttackLibrary instance;
    private void Awake()
    {
        instance = this;
    }
}
