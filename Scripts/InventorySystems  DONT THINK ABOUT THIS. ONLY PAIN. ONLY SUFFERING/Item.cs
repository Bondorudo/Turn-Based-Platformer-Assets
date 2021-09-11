using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType { Charm, Ability, Key, Coin, HealthPotion, SP_Potion }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ability: return ItemAssets.Instance.doubleJumpSprite;
            case ItemType.Charm: return ItemAssets.Instance.fireCharmSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.SP_Potion: return ItemAssets.Instance.sP_PotionSprite;
            case ItemType.Key: return ItemAssets.Instance.keySprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Key:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.SP_Potion:
                return true;
            case ItemType.Ability:
            case ItemType.Charm:
                return false;
        }
    }
}
