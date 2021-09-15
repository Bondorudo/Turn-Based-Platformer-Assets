using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformGameManager : MonoBehaviour
{
    public static PlatformGameManager instance;

    private GameObject[] arrOfEnemies;
    public Transform enemyHolder;

    private GameObject player;
    private GameObject inCombatEnemy;
    public GameObject inventoryCanvas;

    public bool isInventoryOpen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        arrOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("Player");
        inCombatEnemy = GameObject.Find(PlayerPrefs.GetString("InCombatEnemy"));


        Debug.Log("Player Win ? : " + StaticGameData.playerWin);

        if (StaticGameData.playerWin)
        {
            // Enemy dies
            StaticGameData.deadEnemies.Add(inCombatEnemy);
            Debug.Log(inCombatEnemy.name);
            BattleLoadState();
        }
        else
        {
            GameLoadState();
        }

        // DOESNT NOT WORK
        for (int i = 0; i < StaticGameData.deadEnemies.Count; i++)
        {
            StaticGameData.deadEnemies[i].GetComponent<Enemy>().isDead = true;
        }

        // TODO: Set enemies isDead to true when they are dead and keep it that way until we want to respawn the enemy.

        for (int i = 0; i < arrOfEnemies.Length; i++)
        {
            if (arrOfEnemies[i].GetComponent<Enemy>().isDead)
                arrOfEnemies[i].SetActive(false);
            else
                arrOfEnemies[i].SetActive(true);
        }
    }


    // Call when player saves the game / when player dies / when player quits and starts again
    public void RespawnEnemies()
    {
        Debug.Log("RESPAWN ENEMIES");
        for (int i = 0; i < arrOfEnemies.Length; i++)
        {
            arrOfEnemies[i].GetComponent<Enemy>().isDead = false;
            arrOfEnemies[i].SetActive(true);
        }

        StaticGameData.deadEnemies.Clear();
    }

    public void SetInventory(bool boolean)
    {
        isInventoryOpen = boolean;
        inventoryCanvas.SetActive(boolean);
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
    public void GameSaveState()
    {
        RespawnEnemies();

        string s = "";
        s += player.transform.position.x.ToString() + "|";
        s += player.transform.position.y.ToString() + "|";

        PlayerPrefs.SetString("GameSaveState", s);
    }

    public void GameLoadState()
    {
        RespawnEnemies();
        string[] data = PlayerPrefs.GetString("GameSaveState").Split('|');

        player.transform.position = new Vector3(float.Parse(data[0]), float.Parse(data[1]));
    }
    #endregion

    #region Save Battle
    public void BattleSaveState()
    {
        string s = "";
        s += player.transform.position.x.ToString() + "|";
        s += player.transform.position.y.ToString() + "|";

        PlayerPrefs.SetString("BattleSaveState", s);
    }

    public void BattleLoadState()
    {
        string[] data = PlayerPrefs.GetString("BattleSaveState").Split('|');

        player.transform.position = new Vector3(float.Parse(data[0]), float.Parse(data[1]));
    }
    #endregion
}
