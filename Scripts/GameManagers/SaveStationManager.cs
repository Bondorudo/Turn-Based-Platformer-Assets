using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStationManager : MonoBehaviour
{
    private bool canInteract = false;

    private PlatformGameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<PlatformGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            gameManager.CloseInventory();
        }
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
