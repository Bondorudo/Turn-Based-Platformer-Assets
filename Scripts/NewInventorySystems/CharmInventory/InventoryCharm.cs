using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryCharm : MonoBehaviour, IPointerClickHandler
{
    public CharmState charmState;

    public CharmObject realCharm;


    public int xSpot;
    public int ySpot;

    // Check If player clicked a charm, then do something based on charm clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.tag == "Charm")
            CheckCharmState();
    }

    private void Update()
    {
        if (realCharm != null)
        {
            charmState = realCharm.charmState;
        }
    }

    public void CheckCharmState()
    {
        /* If charm is unlocked and not equipped and click, and player can still equip more, then equip charm.
             * 
             * If charm is unlocekd and equipped and click, unequip it.
             * 
             * If charm is locked, tell the player charm is locked and then do nothing
             */


        // Check charm state and do something according to it
        switch (realCharm.charmState)
        {
            case CharmState.Available:
                EquipCharm();
                break;
            case CharmState.Equipped:
                UnequipCharm();
                break;
            case CharmState.Locked:
                CharmIsLocked();
                break;
        }
    }

    public void LoadInventoryCharmsToInventory()
    {
        InventoryManager.instance.EquipCharm(gameObject);
    }

    private void EquipCharm()
    {
        // Add charm to list of equipped charms, reload inventory to see the change.

        charmState = CharmState.Equipped;
        realCharm.charmState = CharmState.Equipped;

        InventoryManager.instance.EquipCharm(gameObject);
    }

    private void UnequipCharm()
    {
        // Remove charm from list of equipped charms, load inventory again to add it back in

        charmState = CharmState.Available;
        realCharm.charmState = CharmState.Available;

        InventoryManager.instance.RemoveCharm(gameObject);
    }

    // Play a small animation and some sound to tell the player charm isnt unlocked, or something else
    private void CharmIsLocked()
    {

    }

    public void UnlockCharm()
    {
        charmState = CharmState.Available;
        realCharm.charmState = CharmState.Available;
    }
}
