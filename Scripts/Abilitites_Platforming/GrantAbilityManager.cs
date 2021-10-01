using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantAbilityManager : Interactable
{
    protected Player player;


    protected SpriteRenderer sprite;

    protected virtual void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>();
    }


    protected override void Interact()
    {
        if (canInteract)
        {
            base.Interact();
            GrantAbility();
            SetHasInteracted();
        }
    }


    protected virtual void GrantAbility()
    {
        Debug.Log("Player got : " + gameObject.name);
    }

    protected void ChangeColor()
    {
        Color color = new Color(0.5f, 0.5f, 0.5f, 1);
        sprite.color = color;
    }
}
