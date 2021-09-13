using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    // Add all charms to this list
    public List<Charm> listOfCharms = new List<Charm>();

    private List<GameObject> listOfEquippedCharms = new List<GameObject>();

    private List<RectTransform> charmRectTransformList = new List<RectTransform>();
    private List<InventoryCharm> inventortCharmList = new List<InventoryCharm>();

    public Transform pfCharmSlot;
    public Transform parentOfEquippedCharms;
    public Transform parentOfAllCharms;


    private RectTransform charmRectTransform;
    private InventoryCharm inventoryCharm;

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
        int x = 0;
        int y = 0;
        float charmSlotCellSize = 90f;

        listOfEquippedCharms.Add(charmToEquip);

        for (int i = 0; i < listOfEquippedCharms.Count; i++)
        {
            charmToEquip.transform.SetParent(parentOfEquippedCharms);
            charmToEquip.GetComponent<RectTransform>().anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
            x++;
        }

        /*
        for (int i = 0; i < listOfCharms.Count; i++)
        {
            if (inventortCharmList[i].charmState == CharmState.Equipped)
            {
                listOfEquippedCharms.Add(inventortCharmList[i]);
                // Move the charm to equipped slot
                listOfEquippedCharms[i].transform.SetParent(parentOfEquippedCharms);
                charmRectTransformList[i].anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
                x++;
            }
        }
        */
    }
    #endregion

    #region AvailableCharms

    // When charm is equipped and we want to Unequip charm it;
    public void RemoveCharm(GameObject charmToRemove)
    {
        int x = 0;
        int y = 0;
        float charmSlotCellSize = 90f;

        listOfEquippedCharms.Remove(charmToRemove);

        
        for (int i = 0; i < listOfCharms.Count; i++)
        {
            if (inventortCharmList[i].charmState == CharmState.Available)
            {
                charmRectTransformList[i].transform.SetParent(parentOfAllCharms);
            }

            //charmToRemove.transform.SetParent(parentOfAllCharms);
            //charmToRemove.GetComponent<RectTransform>().anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);

            // Assign correct values for each charm;
            inventortCharmList[i].charmSprite = listOfCharms[i].charmSprite;
            //newCharm.charmState = listOfCharms[i].charmState;

            // Find Image and change sprite if charm is unlocked. if charm is locked it should have default locked Sprite.
            Image img = charmRectTransformList[i].Find("Image").GetComponent<Image>();
            if (inventortCharmList[i].charmState == CharmState.Available)
                img.sprite = inventortCharmList[i].charmSprite;

            // Set correct positions for the charm objects
            charmRectTransformList[i].anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);
            x++;
            if (x >= 6)
            {
                x = 0;
                y--;
            }
        }
        
    }
    #endregion

    #region CreateEmpty
    private void CreateEmptyCharms()
    {
        int x = 0;
        int y = 0;
        float charmSlotCellSize = 90f;

        for (int i = 0; i < listOfCharms.Count; i++)
        {
            // Create empty charms
            RectTransform n_charm = Instantiate(pfCharmSlot, parentOfAllCharms).GetComponent<RectTransform>();
            n_charm.anchoredPosition = new Vector2(15 + x * charmSlotCellSize, -15 + y * charmSlotCellSize);

            // Create new charm
            charmRectTransform = Instantiate(pfCharmSlot, parentOfAllCharms).GetComponent<RectTransform>();
            inventoryCharm = charmRectTransform.GetComponent<InventoryCharm>();

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
        }
    }
    #endregion
}

