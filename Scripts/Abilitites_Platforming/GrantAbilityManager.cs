using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantAbilityManager : MonoBehaviour
{
    private bool canGrantAbility = false;

    protected PlayerData playerData;

    protected Player player;

    protected SpriteRenderer sprite;

    protected virtual void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canGrantAbility = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canGrantAbility = false;
        }
    }

    private void Update()
    {
        CheckCanGrantAbility();
    }

    private void CheckCanGrantAbility()
    {
        if (canGrantAbility && Input.GetKeyDown("f"))
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
