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
            if (canInteract)
            {
                base.Interact();
                if (!inventoryManager.isInventoryOpen)
                {
                    // Save Game and show inventory, player can close this with pressing f again or moving out of it.
                    inventoryManager.OpenInventory(0);
                    Debug.Log("Save Station Open Inventory");
                    gameManager.GameSaveState(transform.position);
                }
                else
                {
                    inventoryManager.CloseInventory();
                    Debug.Log("Save Station Close Inventory");
                }
            }
            else
            {
                if (!inventoryManager.isInventoryOpen)
                {
                    inventoryManager.OpenInventory(1);
                    Debug.Log("Gameplay Open Inventory");
                }
                else
                {
                    inventoryManager.CloseInventory();
                    Debug.Log("Gameplay Close Inventory");
                }
            }
        }
    }
}
