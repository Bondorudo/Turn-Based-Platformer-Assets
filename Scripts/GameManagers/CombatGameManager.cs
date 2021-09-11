using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
     *      
     *      
     *      
     *      GameObject enemyPrefabista list of a list:
     *      have a list with all enemies in an area
     *      and put that list in another list that has all the area listss in it
     */

    public static CombatGameManager instance;

    public GameState gameState;

    public CombatPlayer player;
    public CombatEnemy actingEnemy;

    public GameObject targetIndicator;
    [SerializeField] private GameObject enemyPrefab;

    private Vector3[] spawnPositions = new Vector3[4];

    public List<GameObject> listOfCurrentEnemies = new List<GameObject>();
    public List<GameObject> listOfAllEnemies = new List<GameObject>();
    /*
     * enemies have an enum type || location.
     * 
     * platform enemy has a public enum to select which type || location enemies we want in combat.
     * 
     * all enemies are in a single list
     * 
     * we spawn random enemies from this list as long as enemys type and type we want match.
     * 
     * 
     */

    private int combatAdv;
    private int enemyIndex;

    public bool enemyLargeAdvantage;
    private bool enemyAvantageTurnOver;

    private void Awake()
    {
        instance = this; 
        
        player = GameObject.Find("Player").GetComponent<CombatPlayer>();
    }

    private void Start()
    {
        combatAdv = StaticGameData.combatAdvantage;
        enemyIndex = 0;

        spawnPositions[0] = new Vector3(6.75f, -2, 0);
        spawnPositions[1] = new Vector3(5, 0.75f, 0);
        spawnPositions[2] = new Vector3(2.25f, 2.5f, 0);
        spawnPositions[3] = new Vector3(-0.5f, 3.25f, 0);

        gameState = GameState.PlayerTurn;

        DetermineAdvantage();
        SetUpEnemyStats();
    }

    private void Update()
    {
        Debug.Log("CURRENT STATE: " + gameState);

        if (listOfCurrentEnemies.Count <= 0)
        {
            Debug.Log("Player WIN");
            // TODO: Go to platform scene, reward player with money, remove platform enemy.
            SceneManager.LoadScene("PlatformScene");
        }
        if (player.isPlayerDead)
        {
            Debug.Log("Player Died");
            // TODO: Fade to black and then, Load last save.
            SceneManager.LoadScene("PlatformScene");
        }

        if (gameState == GameState.EnemyTurn)
        {
            EnemyTurn();
            player.actionCount = player.baseActionCount;
        }
    }

    public Transform GetActingEnemy()
    {
        return actingEnemy.transform;
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
            GameObject enemy = Instantiate(listOfAllEnemies[Random.Range(0, listOfAllEnemies.Count)], spawnPositions[i], transform.rotation);
            CombatEnemy cEnemy = enemy.GetComponent<CombatEnemy>();
            {
                cEnemy.moneyToGive = StaticGameData.enemyMoneyToGive;
                cEnemy.baseDamage = StaticGameData.enemyBaseDmg;
                cEnemy.maxHealth = StaticGameData.enemyMaxHealth;
            }
            if (cEnemy.enemyLocation == StaticGameData.enemyLocation)
            {
                listOfCurrentEnemies.Add(enemy);
            }
            else
                i--;
        }
        /*
        while (listOfCurrentEnemies.Count <= StaticGameData.enemyAmount)
        {
            GameObject enemy = Instantiate(listOfAllEnemies[Random.Range(0, listOfAllEnemies.Count)], spawnPositions[i], transform.rotation);
            CombatEnemy cEnemy = enemy.GetComponent<CombatEnemy>();
            {
                cEnemy.moneyToGive = StaticGameData.enemyMoneyToGive;
                cEnemy.baseDamage = StaticGameData.enemyBaseDmg;
                cEnemy.maxHealth = StaticGameData.enemyMaxHealth;
            }
            if (cEnemy.enemyLocation == StaticGameData.enemyLocation)
            {
                listOfCurrentEnemies.Add(enemy);
            }
        }*/

        enemyAvantageTurnOver = true;
    }

    private void EnemyTurn()
    {
        /* Kun on EnemyTurn niin checkataan onko enemy advantage.
         *  - jos on niin enemyTrunin aikana kaikki viholliset hyˆkk‰‰v‰t kerran.
         *  
         *  - jos ei ole silloin vihollinen[0] hyˆkk‰‰
         *      -> player turn -> vihollinen[1] hyˆkk‰‰
         *              ja menn‰‰‰n kaikki viholliset l‰pi yksi kerrallaan 
         */

        // If is not player turn and enemies dont have advantage
        if (enemyIndex >= listOfCurrentEnemies.Count)
            enemyIndex = 0;

        if (!enemyLargeAdvantage)
        {
            actingEnemy = listOfCurrentEnemies[enemyIndex].gameObject.GetComponent<CombatEnemy>();
            actingEnemy.SelectAbilityToUse();
            enemyIndex++;
            
        }

        // If is not player turn and enemies do have advantage
        else if (enemyLargeAdvantage)
        {
            // all enemies do some action

            StartCoroutine(EnemyAdvantage());
        }
    }

    private IEnumerator EnemyAdvantage()
    {
        for (int i = 0; i < listOfCurrentEnemies.Count; i++)
        {
            actingEnemy = listOfCurrentEnemies[i].gameObject.GetComponent<CombatEnemy>();
            actingEnemy.SelectAbilityToUse();
            yield return new WaitForSeconds(1);
        }

        gameState = GameState.PlayerTurn;
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
                {
                    gameState = GameState.EnemyTurn;
                }
                break;
            case GameState.EnemyAnimation:
                // Jos enemy advantage, silloin kaikki viholliset attackaavat ja sen j‰lkeen menn‰‰n pelaajaan
                // Jos ei ole advantagea, silloin yksi vihollinen attackkaa ja sen j‰lkeen menn‰‰n pelaajaan;
                if (enemyLargeAdvantage)
                    break;
                gameState = GameState.PlayerTurn;
                break;
        }
    }
}
