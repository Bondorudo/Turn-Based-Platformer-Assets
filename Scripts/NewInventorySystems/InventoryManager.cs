using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    // Add all charms to this list
    public List<Charm> listOfCharms = new List<Charm>();

    public List<GameObject> listOfEquippedCharms = new List<GameObject>();

    private List<RectTransform> charmRectTransformList = new List<RectTransform>();
    public List<InventoryCharm> inventortCharmList = new List<InventoryCharm>();

    public Transform pfCharmSlot;
    public Transform parentOfEquippedCharms;
    public Transform parentOfAllCharms;

    private RectTransform charmRectTransform;
    private InventoryCharm inventoryCharm;

    float charmSlotCellSize = 90f;

    int charmSpotX = 0;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateEmptyCharms();
    }

    #region EquippedCharms

    // Charm is not not equipped, so then equip it.
    public void EquipCharm(GameObject charmToEquip)
    {
        // If equipped charm count is less than max charm amount equip a charm, else thell the player they have too many charms
        if (listOfEquippedCharms.Count <= 2)
        {
            // Set up charm, then add it to the list of equippped charms. and increment charmpX spot by 1.
            charmToEquip.transform.SetParent(parentOfEquippedCharms);
            charmToEquip.GetComponent<InventoryCharm>().charmState = CharmState.Equipped;
            charmToEquip.GetComponent<RectTransform>().anchoredPosition = new Vector2(15 + charmSpotX * charmSlotCellSize, -15);

            listOfEquippedCharms.Add(charmToEquip);

            charmSpotX++;
        }
        else
            Debug.Log("MAX CHARMS");
    }
    #endregion

    // When player removes 1 charm, all equipped charms are reorganized so that they go from left to right without gaps.
    public void ReorganizeListOfEquippedCharms()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < listOfEquippedCharms.Count; i++)
        {
            listOfEquippedCharms[i].transform.SetParent(parentOfEquippedCharms);
            listOfEquippedCharms[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
            x++;
        }
    }

    #region AvailableCharms

    // When charm is equipped and we want to Unequip charm it;
    public void RemoveCharm(GameObject charmToRemove)
    {
        // when removing charms decrement charmX spot by 1.
        charmSpotX--;

        // Remove charm from list and reorganize equipped charms
        listOfEquippedCharms.Remove(charmToRemove);
        ReorganizeListOfEquippedCharms();

        // Throw removed charm back into all charms area, to its correct spot, by going trough all charms.
        for (int i = 0; i < listOfCharms.Count; i++)
        {
            if (inventortCharmList[i].charmState == CharmState.Available)
            {
                charmRectTransformList[i].transform.SetParent(parentOfAllCharms);

                // Set correct positions for the charm objects
                charmRectTransformList[i].anchoredPosition = new Vector2(15 + inventortCharmList[i].xSpot * charmSlotCellSize, -15 + inventortCharmList[i].ySpot * charmSlotCellSize);
                
            }
        }
    }
    #endregion

    #region CreateEmpty

    // At start create empty charm slots to show locked and equipped charms as empty.
    // Also create all actual charms that can be equipped.
    private void CreateEmptyCharms()
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < listOfCharms.Count; i++)
        {
            // Create empty charms
            RectTransform n_charm = Instantiate(pfCharmSlot, parentOfAllCharms).GetComponent<RectTransform>();
            n_charm.anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
            n_charm.GetComponent<InventoryCharm>().charmState = CharmState.Locked;

            // Create new charm
            charmRectTransform = Instantiate(pfCharmSlot, parentOfAllCharms).GetComponent<RectTransform>();
            inventoryCharm = charmRectTransform.GetComponent<InventoryCharm>();
            inventoryCharm.xSpot = x;
            inventoryCharm.ySpot = y;
            inventoryCharm.realCharm = listOfCharms[i];

            // Set correct positions for the charm objects
            charmRectTransform.anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
            
            charmRectTransformList.Add(charmRectTransform);
            inventortCharmList.Add(inventoryCharm);

            x++;
            if (x >= 6)
            {
                x = 0;
                y--;
            }

            if (listOfCharms[i].charmState != CharmState.Locked)
            {
                // Find Image and change sprite if charm is unlocked. if charm is locked it should have default locked Sprite.
                charmRectTransformList[i].Find("Image").GetComponent<Image>().sprite = listOfCharms[i].charmSprite;

                inventortCharmList[i].UnlockCharm();
            }
        }
    }
    #endregion

    #region UpdateCharms

    // Call when player unlocks a new charm
    public void UpdateUnlockedCharms()
    {
        for (int i = 0; i < listOfCharms.Count; i++)
        {
            if (listOfCharms[i].charmState != CharmState.Locked)
            {
                // Find Image and change sprite if charm is unlocked. if charm is locked it should have default locked Sprite.
                charmRectTransformList[i].Find("Image").GetComponent<Image>().sprite = listOfCharms[i].charmSprite;

                inventortCharmList[i].UnlockCharm();
            }
        }
    }
    #endregion
}

