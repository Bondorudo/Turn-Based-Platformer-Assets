using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGameManager : MonoBehaviour
{
    public static PlatformGameManager instance;

    public Player player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void CreateText()
    {
        bool isCritical = Random.Range(0, 100) < 30;
        DamagePopup.Create(Vector3.zero, 252, 30);
    }
}
