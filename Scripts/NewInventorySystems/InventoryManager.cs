using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryObject inventory;

    // Add all charms to this list
    public List<CharmObject> listOfCharms = new List<CharmObject>();
    public List<GameObject> listOfEquippedCharms = new List<GameObject>();

    private List<RectTransform> charmRectTransformList = new List<RectTransform>();
    public List<InventoryCharm> inventoryCharmList = new List<InventoryCharm>();


    public List<GameObject> inventoryPages = new List<GameObject>();

    public Transform pfCharmSlot;
    public Transform parentOfEquippedCharms;
    public Transform parentOfAllCharms;

    private RectTransform charmRectTransform;
    private InventoryCharm inventoryCharm;

    float charmSlotCellSize = 90f;

    int charmSpotX = 0;

    public bool isInventoryOpen;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory.container.UnlockedCharms.Clear();

        CreateEmptyCharms();
        CloseInventory();

        AddUnlockedCharmsToList();
    }
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            // If 'e' open inventory
            if (!isInventoryOpen)
            {
                OpenInventory(1);
            }
            else
            {
                CloseInventory();
            }
        }
    }

    #region ---GENERAL---

    int pageIndex = 0;
    public void InventoryScrollButton(int i)
    {
        for (int y = 0; y < inventoryPages.Count; y++)
        {
            inventoryPages[y].SetActive(false);
        }

        pageIndex += i;
        if (pageIndex >= inventoryPages.Count)
        {
            pageIndex = 0;
        }
        else if (pageIndex < 0)
        {
            pageIndex = inventoryPages.Count -1;
        }

        inventoryPages[pageIndex].SetActive(true);
    }

    public void OpenInventory(int pageIndex)
    {
        isInventoryOpen = true;
        this.pageIndex = pageIndex;
        inventoryPages[pageIndex].SetActive(true);
    }

    public void CloseInventory()
    {
        for (int i = 0; i < inventoryPages.Count; i++)
        {
            inventoryPages[i].SetActive(false);
        }
        isInventoryOpen = false;
    }

    #endregion

    #region ---CHARMS---

    private void AddUnlockedCharmsToList()
    {
        for (int i = 0; i < listOfCharms.Count; i++)
        {
            if (listOfCharms[i].charmState != CharmState.Locked)
            {
                inventory.AddUnlockedCharmToInventory(new Charm(listOfCharms[i]));
            }
        }
    }

    #region EquippedCharms

    public void UpdateEquippedCharms()
    {
        for (int i = 0; i < inventoryCharmList.Count; i++)
        {
            inventoryCharmList[i].CheckCharmState();
        }
    }

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


        PopulateCharmList();
    }

    public void PopulateCharmList()
    {
        inventory.container.EquippedCharms.Clear();

        for (int i = 0; i < listOfEquippedCharms.Count; i++)
        {
            inventory.AddEquippedCharmToInventory(new Charm(listOfEquippedCharms[i].GetComponent<InventoryCharm>().realCharm));
        }
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

        PopulateCharmList();
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
            if (inventoryCharmList[i].charmState == CharmState.Available)
            {
                charmRectTransformList[i].transform.SetParent(parentOfAllCharms);

                // Set correct positions for the charm objects
                charmRectTransformList[i].anchoredPosition = new Vector2(15 + inventoryCharmList[i].xSpot * charmSlotCellSize, -15 + inventoryCharmList[i].ySpot * charmSlotCellSize);
                
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
            inventoryCharmList.Add(inventoryCharm);

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

                inventoryCharmList[i].UnlockCharm();
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

                inventoryCharmList[i].UnlockCharm();
            }
        }
    }
    #endregion
    #endregion

    #region ---ITEMS/SKILLS---

    #endregion
}

