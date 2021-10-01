using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmItem : Interactable
{
    private bool hasGrantedCharm = false;

    public Charm charmToGive;


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

        InventoryManager.instance.UpdateUnlockedCharms();

        Destroy(gameObject);
    }
}
