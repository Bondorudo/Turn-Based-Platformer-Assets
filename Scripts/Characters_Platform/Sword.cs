using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sword : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Enemy" && gameObject.tag == "PlayerSword")
        {
            // if hits enemy go to battle scene

            gameManager.GoIntoCombat(2, 0);
            PlayerPrefs.SetString("InCombatEnemy", coll.gameObject.name);
        }

        if (coll.tag == "Player" && gameObject.tag != "Player")
        {
            gameManager.GoIntoCombat(4, 2);
        }
    }
}
