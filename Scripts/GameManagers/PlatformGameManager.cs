using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformGameManager : MonoBehaviour
{
    public static PlatformGameManager instance;

    private GameObject[] arrOfEnemies;

    private GameObject playerObject;
    private Player playerScript;
    public GameObject inventoryCanvas;


    string enemyDeadString = "";


    private void Awake()
    {
        instance = this;
        arrOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Get this string at start of game
        enemyDeadString = PlayerPrefs.GetString("enemyDeadString");

        // Find game objects
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<Player>();
        StaticGameData.inCombatEnemy = GameObject.Find(PlayerPrefs.GetString("InCombatEnemy"));
    }

    private void Start()
    {
        CombatOutcome();
    }

    public void CombatOutcome()
    {
        if (StaticGameData.playerWin)
        {
            BattleLoadState();

            ThrowMoney();

            KillEnemies();
        }
        else
        {
            GameLoadState();
        }
        StaticGameData.playerWin = false;
    }

    private void ThrowMoney()
    {
        // This works for testing, but
        // TODO: Instantiate coins and throw them around the dead enemy, player can then pick those up to gain money.
        playerScript.ChangeMoneyAmount(StaticGameData.inCombatEnemy.GetComponent<Enemy>().GiveMoney());
    }

    public void KillEnemies()
    {
        // Add to enemyDeadString combat enemys name and set string
        enemyDeadString += StaticGameData.inCombatEnemy.name + "|";
        PlayerPrefs.SetString("enemyDeadString", enemyDeadString);

        // Split all enemy names, then go trough all of them and find their game objects and get their Enemy component. then set active to false
        string[] data = PlayerPrefs.GetString("enemyDeadString").Split('|');

        for (int i = 0; i < data.Length; i++)
        {
            GameObject.Find(data[i]).SetActive(false);
        }
    }

    // Call when player saves the game / when player dies / when player quits and starts again
    public void RespawnEnemies()
    {
        // Go trough array of enemies and for each set active to true
        foreach (var enemy in arrOfEnemies)
        {
            enemy.SetActive(true);
        }

        PlayerPrefs.SetString("enemyDeadString", "");
    }

    public void GoIntoCombat(int enemyAmount, int advantage)
    {
        // 0 = player adv | 1 = neutral adv | 2 = enemy adv
        StaticGameData.enemyAmount = enemyAmount;
        StaticGameData.combatAdvantage = advantage;

        BattleSaveState();

        SceneManager.LoadScene("CombatScene");
    }

    // TODO: Also save powerups 

    #region Save Game
    public void GameSaveState(Vector2 pos)
    {
        RespawnEnemies();

        string s = "";
        s += pos.x.ToString() + "|";
        s += pos.y.ToString() + "|";
        s += playerScript.moneyAmount.ToString();

        PlayerPrefs.SetString("GameSaveState", s);
    }

    public void GameLoadState()
    {
        RespawnEnemies();
        string[] data = PlayerPrefs.GetString("GameSaveState").Split('|');

        playerObject.transform.position = new Vector3(float.Parse(data[0]), float.Parse(data[1]));
        playerScript.ChangeMoneyAmount(int.Parse(data[2]));
    }
    #endregion

    #region Save Battle
    public void BattleSaveState()
    {
        string s = "";
        s += playerObject.transform.position.x.ToString() + "|";
        s += playerObject.transform.position.y.ToString() + "|";
        s += playerScript.moneyAmount.ToString();

        PlayerPrefs.SetString("BattleSaveState", s);
    }

    public void BattleLoadState()
    {
        string[] data = PlayerPrefs.GetString("BattleSaveState").Split('|');

        playerObject.transform.position = new Vector3(float.Parse(data[0]), float.Parse(data[1]));
        playerScript.ChangeMoneyAmount(int.Parse(data[2]));
    }
    #endregion

}
