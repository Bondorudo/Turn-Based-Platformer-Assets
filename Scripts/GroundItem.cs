using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : Interactable
{
    public ItemObject item;

    protected override void Interact()
    {
        if (canInteract)
        {
            base.Interact();
            // TODO: Add item to inventory.
        }
    }
    //TODO: Create coins, that the player can just pick up by walking over them, no interraction with 'f' needed.
}
