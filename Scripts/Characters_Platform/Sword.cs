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

            // 0 = player adv
            StaticGameData.combatAdvantage = 0;
            StaticGameData.enemyAmount = 1;

            SceneManager.LoadScene("CombatScene");
        }

        if (coll.tag == "Player" && gameObject.tag != "Player")
        {
            // 2 = enemy adv
            StaticGameData.combatAdvantage = 2;
            StaticGameData.enemyAmount = 3;

            SceneManager.LoadScene("CombatScene");
        }
    }
}
