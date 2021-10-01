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

    private void Update()
    {
        CheckCanGrantAbility();
    }

    private void CheckCanGrantAbility()
    {
        if (canInteract && Input.GetKeyDown("f"))
        {
            GrantAbility();
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
