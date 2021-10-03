using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : Interactable
{
    public ItemObject item;
    public InventoryObject inventory;

    protected override void Interact()
    {
        if (canInteract)
        {
            base.Interact();
            // Add item to inventory.
            inventory.AddItemToInventory(new Item(item), 1);
            Destroy(this.gameObject);
        }
    }
    //TODO: Create coins, that the player can just pick up by walking over them, no interraction with 'f' needed.
}
