using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStationManager : Interactable
{
    private PlatformGameManager gameManager;
    private InventoryManager inventoryManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<PlatformGameManager>();
        inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    protected override void PlayerExitTrigger()
    {
        base.PlayerExitTrigger();
        inventoryManager.CloseInventory();

    }


    protected override void Interact()
    {
        if (canInteract)
        {
            base.Interact();
            if (!inventoryManager.isInventoryOpen)
            {
                // Save Game and show inventory, player can close this with pressing f again or moving out of it.
                inventoryManager.OpenInventory(0);
                gameManager.GameSaveState(transform.position);
            }
            else
            {
                inventoryManager.CloseInventory();
            }
        }
    }
}
