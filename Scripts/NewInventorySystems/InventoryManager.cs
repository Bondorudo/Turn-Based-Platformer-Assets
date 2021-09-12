using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Add all charms to this list
    public List<Charm> listOfCharms = new List<Charm>();

    public Transform pfCharmSlot;
    public Transform parentOfCharm;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCharmInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpCharmInventory()
    {
        int x = 0;
        int y = 0;
        float charmSlotCellSize = 90f;

        for (int i = 0; i < listOfCharms.Count; i++)
        {
            // Create new charm
            RectTransform charm = Instantiate(pfCharmSlot, parentOfCharm).GetComponent<RectTransform>();
            InventoryCharm newCharm = charm.GetComponent<InventoryCharm>();

            // Assign correct values for each charm;
            newCharm.charmSprite = listOfCharms[i].charmSprite;
            newCharm.charmState = listOfCharms[i].charmState;

            // Find Image and change sprite if charm is unlocked. if charm is locked it should have default locked Sprite.
            Image img = charm.Find("Image").GetComponent<Image>();
            if (newCharm.charmState != CharmState.Locked)
                img.sprite = newCharm.charmSprite;

            // Set correct positions for the charm objects
            charm.anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
            x++;
            if (x >= 6)
            {
                x = 0;
                y--;
            }
        }
    }
}
