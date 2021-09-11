using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [Header("ItemInTheWorld")]
    public Transform pfItemWorld;

    [Header("Charms")]
    public Sprite fireCharmSprite;

    [Header("Abilities")]
    public Sprite doubleJumpSprite;

    [Header("Other Items")]
    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite sP_PotionSprite;
    public Sprite coinSprite;
    public Sprite keySprite;
}
