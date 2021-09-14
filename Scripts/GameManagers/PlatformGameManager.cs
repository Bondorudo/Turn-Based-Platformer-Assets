using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGameManager : MonoBehaviour
{
    public static PlatformGameManager instance;

    public List<GameObject> listOfEnemies = new List<GameObject>();
    public Transform enemyHolder;

    public GameObject inventoryCanvas;

    public bool isInventoryOpen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    // Call when player saves the game / when player dies / when player quits and starts again
    public void RespawnEnemies()
    {
        for (int i = 0; i < listOfEnemies.Count; i++)
        {
            Instantiate(listOfEnemies[i], enemyHolder);
        }
    }

    public void SetInventory(bool boolean)
    {
        Debug.Log("SET INVENTORT : " + boolean);
        isInventoryOpen = boolean;
        inventoryCanvas.SetActive(boolean);
    }
}
