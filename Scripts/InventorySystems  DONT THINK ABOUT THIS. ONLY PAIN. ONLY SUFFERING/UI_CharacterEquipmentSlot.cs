using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CharacterEquipmentSlot : MonoBehaviour, IDropHandler
{
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs : EventArgs
    {
        public Item item;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item item = UI_ItemDrag.instance.GetItem();
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { item = item});
    }
}
