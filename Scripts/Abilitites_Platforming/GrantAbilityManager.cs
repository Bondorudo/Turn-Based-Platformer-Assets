using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantAbilityManager : MonoBehaviour
{
    private bool canGrantAbility = false;

    protected GameData gameData;
    protected PlayerData playerData;

    protected Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
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
}
