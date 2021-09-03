using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PlayerTurn, PlayerAnimation, EnemyTurn, EnemyAnimation }
public class CombatGameManager : MonoBehaviour
{
    /*
     * List of enemies
     * at start add to list enemies of enemy amount
     * then we have list of enemies that has for example 3 units in it
     * instanciate those 3 units
     * 
     * -> player advantage
     *      player gets extra actions
     *          increase action couunt
     *          
     *     from list of enemies pick 0th unit, call its do action
     *      increment list index, and then end turn.
     * 
     * 
     * -> neutral advantage
     *      from list of enemies pick 0th unit, call its do action
     *      increment list index, and then end turn.
     * 
     * 
     * -> enemy advantage
     *      for each enemy in scene. in this case 3, call do action for all of them
     *      then call end turn
     */

    public static CombatGameManager instance;

    public GameState gameState;

    /* GameObject enemyPrefabista list of a list:
     * have a list with all enemies in an area
     * 
     * and put that list in another list that has all the area listss in it
     */
    private CombatPlayer player;
    [SerializeField] private GameObject enemyPrefab;
    private Vector3[] spawnPositions = new Vector3[4];

    private List<GameObject> listOfCurrentEnemies = new List<GameObject>();
    GameObject[] enemies;

    private int combatAdv;
    private int enemyIndex;

    public bool enemyLargeAdvantage;

    private void Awake()
    {
        instance = this; 
        
        player = GameObject.Find("Player").GetComponent<CombatPlayer>();
    }

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        combatAdv = StaticGameData.combatAdvantage;
        enemyIndex = 0;

        spawnPositions[0] = new Vector3(6.75f, -2, 0);
        spawnPositions[1] = new Vector3(5, 0.75f, 0);
        spawnPositions[2] = new Vector3(2.25f, 2.5f, 0);
        spawnPositions[3] = new Vector3(-0.5f, 3.25f, 0);

        gameState = GameState.PlayerTurn;

        DetermineAdvantage();
        SetUpEnemyStats();

        foreach (GameObject e in enemies)
        {
            Debug.Log(e.name);
        }
    }

    private void Update()
    {
        EnemyTurn();

        Debug.Log("CURRENT STATE: " + gameState);
    }

    private void DetermineAdvantage()
    {
        switch (combatAdv)
        {
                // case 0 = player advantage
            case 0:
                gameState = GameState.PlayerTurn;
                enemyLargeAdvantage = false;
                break;
                // case 1 = neutral advantage
            case 1:
                gameState = GameState.EnemyTurn;
                enemyLargeAdvantage = false;
                break;
                // case 2 = enemy advantage
            case 2:
                gameState = GameState.EnemyTurn;
                enemyLargeAdvantage = true;
                break;
                // if something breaks go to default neutral advantage
            default:
                gameState = GameState.EnemyTurn;
                enemyLargeAdvantage = false;
                break;
        }
    }

    private void SetUpEnemyStats()
    {
        for (int i = 0; i < StaticGameData.enemyAmount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPositions[i], transform.rotation);
            CombatEnemy cEnemy = enemy.GetComponent<CombatEnemy>();
            {
                cEnemy.level = StaticGameData.enemyLevel;
                cEnemy.xpToGive = StaticGameData.enemyXpToGive;
                cEnemy.baseDamage = StaticGameData.enemyBaseDmg;
            }
            listOfCurrentEnemies.Add(enemy);
        }
    }

    private void EnemyTurn()
    {
        // If is not player turn and enemies dont have advantage
        if (gameState == GameState.EnemyTurn && !enemyLargeAdvantage)
        {
            // one enemy does an action
            Debug.Log(enemyIndex);
            listOfCurrentEnemies[enemyIndex].GetComponent<CombatEnemy>().Action();
            enemyIndex++;
            if (enemyIndex >= listOfCurrentEnemies.Count)
                enemyIndex = 0;

            Debug.Log("1 enemy attacks");
            gameState = GameState.PlayerTurn;
        }

        // If is not player turn and enemies do have advantage
        else if (gameState == GameState.EnemyTurn && enemyLargeAdvantage)
        {
            // all enemies do some action
            foreach  (GameObject enemy in listOfCurrentEnemies)
            {
                enemy.GetComponent<CombatEnemy>().Action();
            }
            Debug.Log("All enemies attack");
            gameState = GameState.PlayerTurn;
        }
    }

    public void EndAnimation()
    {
        switch (gameState)
        {
            case GameState.PlayerAnimation:
                // Jos pelaajalla on action counttea j‰ljell‰ mene Player turniin
                // Toisin mene Enemy Turniin
                if (player.actionCount > 0)
                {
                    gameState = GameState.PlayerTurn;
                }
                else
                    gameState = GameState.EnemyTurn;
                break;
            case GameState.EnemyAnimation:
                // Jos enemy advantage, silloin kaikki viholliset attackaavat ja sen j‰lkeen menn‰‰n pelaajaan
                // Jos ei ole advantagea, silloin yksi vihollinen attackkaa ja sen j‰lkeen menn‰‰n pelaajaan;
                gameState = GameState.PlayerTurn;
                break;
        }
    }
}
