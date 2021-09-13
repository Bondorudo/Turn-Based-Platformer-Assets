using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGameManager : MonoBehaviour
{
    public static PlatformGameManager instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    public void CreateText()
    {
        bool isCritical = Random.Range(0, 100) < 30;
        FloatingText.Create(Vector3.zero, 252.ToString(), 30, 3);
    }
}
