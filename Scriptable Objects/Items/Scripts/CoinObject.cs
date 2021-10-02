using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Object", menuName = "Scriptable Objects/Inventory System/Items/Coin")]
public class CoinObject : ItemObject
{
    public int coinValue;

    private void Awake()
    {
        itemType = ItemType.CurrencyItem;
    }
}
