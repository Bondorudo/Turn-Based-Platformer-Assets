using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGameManager : MonoBehaviour
{
    public void CreateText()
    {
        bool isCritical = Random.Range(0, 100) < 30;
        DamagePopup.Create(Vector3.zero, 252, 30);
    }
}
