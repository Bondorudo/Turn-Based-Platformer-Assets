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

    public GameObject targetedEnemy;
    public GameObject targetIndicator;
    [SerializeField] private GameObject enemyPrefab;

    private Vector3[] spawnPositions = new Vector3[4];

    public List<GameObject> listOfCurrentEnemies = new List<GameObject>();

    private GameObject[][] arrayOfEnemies = new GameObject[10][];
    // [0][0] = area 0, enemy 0
    // [0][1] = area 0, enemy 1
    // [1][3] = area 1, enemy 3


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
        }
        // else if player is dead: lose

        if (gameState == GameState.EnemyTurn)
        {
            EnemyTurn();
            player.actionCount = player.baseActionCount;
        }

        targetedEnemy = player.target;
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
                cEnemy.maxHealth = StaticGameData.enemyMaxHealth;
            }
            listOfCurrentEnemies.Add(enemy);
        }
    }

    private void EnemyTurn()
    {
        /* Kun on EnemyTurn niin checkataan onko enemy advantage.
         *  - jos on niin enemyTrunin aikana kaikki viholliset hyˆkk‰‰v‰t kerran.
         *  
         *  - jos ei ole silloin vihollinen[0] hyˆkk‰‰
         *      -> player turn -> vihollinen[1] hyˆkk‰‰
         *              ja menn‰‰‰n kaikki viholliset l‰pi yksi kerrallaan 
         * 
         */

        // If is not player turn and enemies dont have advantage
        if (!enemyLargeAdvantage)
        {
            actingEnemy = listOfCurrentEnemies[enemyIndex].gameObject.GetComponent<CombatEnemy>();
            actingEnemy.SelectAbilityToUse();
            enemyIndex++;
            if (enemyIndex >= listOfCurrentEnemies.Count)
                enemyIndex = 0;
        }

        // If is not player turn and enemies do have advantage
        else if (enemyLargeAdvantage)
        {
            // all enemies do some action

            // Turn is given to player after first enemy finishes attack because the attack has an event to change to player turn

            for (int i = 0; i < listOfCurrentEnemies.Count; i++)
            {
                actingEnemy = listOfCurrentEnemies[i].gameObject.GetComponent<CombatEnemy>();
                actingEnemy.SelectAbilityToUse();
            }
        }
    }

    public void EndAnimation()
    {
        switch (gameState)
        {
            case GameState.PlayerAnimation:
                // Jos pelaajalla on action counttea j‰ljell‰ mene Player turniin
                // Toisin mene Enemy Turniin
                Debug.Log("player or enemy turn? = " + player.actionCount);
                if (player.actionCount > 0)
                {
                    Debug.Log("Player Turn");
                    gameState = GameState.PlayerTurn;
                }
                else
                {
                    Debug.Log("Enemy Turn");
                    gameState = GameState.EnemyTurn;
                }
                break;
            case GameState.EnemyAnimation:
                // Jos enemy advantage, silloin kaikki viholliset attackaavat ja sen j‰lkeen menn‰‰n pelaajaan
                // Jos ei ole advantagea, silloin yksi vihollinen attackkaa ja sen j‰lkeen menn‰‰n pelaajaan;
                gameState = GameState.PlayerTurn;
                break;
        }
    }
}
