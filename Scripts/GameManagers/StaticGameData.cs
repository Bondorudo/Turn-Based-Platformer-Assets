using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameData
{
    // Enemy Info
    public static int combatAdvantage;
    public static int enemyMoneyToGive;
    public static int enemyBaseDmg;
    public static int enemyAmount;
    public static int enemyMaxHealth;
    public static EnemyLocation enemyLocation;

    // Player Info
    public static int playerMaxHealth;
    public static int playerMaxSP;
    public static int playerBaseDamage;
    public static List<Stats> playerDamageAttributes;


    // TODO: other info about combat: enemy types, background, design
}
