using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmItem : MonoBehaviour
{
    private bool canPickUpCharm = false;
    private bool hasGrantedCharm = false;

    public Charm charmToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canPickUpCharm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canPickUpCharm = false;
        }
    }

    private void Update()
    {
        CheckCanGrantCharm();
    }

    private void CheckCanGrantCharm()
    {
        if (canPickUpCharm && Input.GetKeyDown("f") && !hasGrantedCharm)
        {
            GrantCharm();
            hasGrantedCharm = true;
        }
    }

    protected void GrantCharm()
    {
        Debug.Log("Player got : " + gameObject.name);

        charmToGive.charmState = CharmState.Available;

        //InventoryManager.instance.UpdateCharms();

        Destroy(gameObject);
    }
}
