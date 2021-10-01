using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { KeyItem, CurrencyItem, CharmItem, AbilityItem }

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType itemType;
    [TextArea(7, 10)]
    public string desctiption;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;

    public Item(ItemObject item)
    {
        Id = item.Id;
        Name = item.name;
    }
}
