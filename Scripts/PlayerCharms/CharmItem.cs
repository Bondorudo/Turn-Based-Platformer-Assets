using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmItem : Interactable
{
    private bool hasGrantedCharm = false;

    public Charm charmToGive;


    private void Update()
    {
        CheckCanGrantCharm();
    }

    private void CheckCanGrantCharm()
    {
        if (canInteract && Input.GetKeyDown("f") && !hasGrantedCharm)
        {
            GrantCharm();
            hasGrantedCharm = true;
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
