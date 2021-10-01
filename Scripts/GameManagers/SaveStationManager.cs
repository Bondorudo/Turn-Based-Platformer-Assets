using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStationManager : Interactable
{
    private PlatformGameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<PlatformGameManager>();
    }

    protected override void PlayerExitTrigger()
    {
        base.PlayerExitTrigger();
        gameManager.CloseInventory();

    }

    private void Update()
    {
        CheckcanSave();
    }

    private void CheckcanSave()
    {
        if (canInteract && !gameManager.isInventoryOpen && Input.GetKeyDown("f"))
        {
            // Save Game and show inventory, player can close this with pressing f again or moving out of it.
            gameManager.SetInventory(true);
            gameManager.GameSaveState(transform.position);
        }
        else if (canInteract && gameManager.isInventoryOpen && Input.GetKeyDown("f"))
        {
            gameManager.CloseInventory();
        }
    }
}
