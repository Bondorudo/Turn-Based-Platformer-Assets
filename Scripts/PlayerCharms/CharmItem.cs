using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmItem : Interactable
{
    private bool hasGrantedCharm = false;

    public Charm charmToGive;
    public InventoryObject inventory;

    protected override void Interact()
    {
        if (canInteract)
        {
            if (!hasGrantedCharm)
            {
                base.Interact();
                GrantCharm();
                hasGrantedCharm = true;
            }
        }
    }

    private void GrantCharm()
    {
        Debug.Log("Player got : " + gameObject.name);

        charmToGive.charmState = CharmState.Available;
        inventory.container.UnlockedCharms.Add(charmToGive);

        InventoryManager.instance.UpdateUnlockedCharms();

        Destroy(gameObject);
    }
}
