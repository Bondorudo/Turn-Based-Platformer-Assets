using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyItem Object", menuName = "Scriptable Objects/Inventory System/Items/Key")]
public class KeyItemObject : ItemObject
{
    private void Awake()
    {
        itemType = ItemType.KeyItem;
    }
}
