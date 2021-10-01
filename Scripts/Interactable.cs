using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool canInteract;
    protected bool hasInteracted;
    private GameObject showInteractivityUI;

    private void Awake()
    {
        showInteractivityUI = GameObject.Find("InteractivityPanel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!hasInteracted)
            {
                canInteract = true;
                showInteractivityUI.SetActive(true);
                showInteractivityUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            showInteractivityUI.SetActive(false);
            PlayerExitTrigger();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Interact();
            showInteractivityUI.SetActive(false);
        }
    }

    protected virtual void Interact()
    {

    }

    protected virtual void SetHasInteracted()
    {
        hasInteracted = true;
    }

    protected virtual void PlayerExitTrigger()
    {

    }

    // TODO: Move Check for 'f' key press here and then from that do other things, because everything that can be interracted with will be done wit the 'f' key, unless otherwise specified
}
