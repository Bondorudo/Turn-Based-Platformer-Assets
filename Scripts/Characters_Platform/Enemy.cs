using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Fighter
{
    [Header("Combat Stats")]
    [SerializeField] private int dmg;
    [SerializeField] private int maxHealth;
    [SerializeField] private EnemyLocation enemyLocation;
    [SerializeField] private int enemyAmount;

    [Header("Platforming")]
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform ledgeCheckPos;

    [SerializeField] private float ledgeCheckDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistanceToTravel;
    private float startPosX;

    [Range(1,30)]
    public int minMoneyToGive;
    [Range(1, 30)]
    public int maxMoneyToGive;


    private bool ledgeCheck;

    private PlatformGameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<PlatformGameManager>();

        startPosX = transform.position.x;

        //StaticGameData.enemyMoneyToGive = moneyToGive;
        StaticGameData.enemyBaseDmg = dmg;
        StaticGameData.enemyMaxHealth = maxHealth;
        StaticGameData.enemyLocation = enemyLocation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLedge();
        Movement();

        ledgeCheck = Physics2D.Raycast(ledgeCheckPos.position, Vector2.down, ledgeCheckDistance, whatIsGround);
    }

    public int GiveMoney()
    {
        int money = Random.Range(minMoneyToGive, maxMoneyToGive);
        return money;
    }

    private void Movement()
    {
        transform.Translate(moveSpeed * -1, 0, 0);
    }

    private void CheckLedge()
    {
        if (!ledgeCheck)
        {
            Flip();
        }

        if (transform.position.x > startPosX + maxDistanceToTravel)
            Flip();
        else if (transform.position.x < startPosX - maxDistanceToTravel)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.GoIntoCombat(3, 1);
            PlayerPrefs.SetString("InCombatEnemy", gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ledgeCheckPos.position, new Vector2(ledgeCheckPos.position.x, ledgeCheckPos.position.y - ledgeCheckDistance));
        Gizmos.DrawIcon(new Vector3(startPosX + maxDistanceToTravel, transform.position.y, transform.position.z), "sv_icon_dot13_pix16_gizmo", true);
        Gizmos.DrawIcon(new Vector3(startPosX - maxDistanceToTravel, transform.position.y, transform.position.z), "sv_icon_dot13_pix16_gizmo", true);
    }
}
