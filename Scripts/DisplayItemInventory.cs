using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayItemInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public Vector2 startPos;
    public int x_Space_Between_Slots;
    public int y_Space_Between_Slots;
    public int numberOfColumns;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.Items.Count; i++)
        {
            InventorySlot slot = inventory.container.Items[i];

            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {

            }
        }
    }

    public void UpdateDisplay()
    {

    }

    // Gets the supposed position of items in the inventory
    public Vector3 GetPosition(int i)
    {
        return new Vector3(startPos.x + (x_Space_Between_Slots * (i % numberOfColumns)), startPos.y + (-y_Space_Between_Slots * (i / numberOfColumns)), 0f);
    }
}
