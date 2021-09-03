using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Fighter
{
    [Header("Combat Stats")]
    [SerializeField] private int level;
    [SerializeField] private int xpToGive;
    [SerializeField] private int dmg;
    [SerializeField] private List<int> enemyTypes = new List<int>();
    [SerializeField] private int[] enemyAmount = new int[2];

    [Header("Platforming")]
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform ledgeCheckPos;

    [SerializeField] private float ledgeCheckDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistanceToTravel;
    private float startPosX;

    private bool ledgeCheck;


    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;

        StaticGameData.enemyLevel = level;
        StaticGameData.enemyXpToGive = xpToGive;
        StaticGameData.enemyBaseDmg = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLedge();
        Movement();

        ledgeCheck = Physics2D.Raycast(ledgeCheckPos.position, Vector2.down, ledgeCheckDistance, whatIsGround);
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
            // 1 = neutral adv
            StaticGameData.combatAdvantage = 1;
            StaticGameData.enemyAmount = 2;

            SceneManager.LoadScene("CombatScene");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ledgeCheckPos.position, new Vector2(ledgeCheckPos.position.x, ledgeCheckPos.position.y - ledgeCheckDistance));
        Gizmos.DrawIcon(new Vector3(startPosX + maxDistanceToTravel, transform.position.y, transform.position.z), "sv_icon_dot13_pix16_gizmo", true);
        Gizmos.DrawIcon(new Vector3(startPosX - maxDistanceToTravel, transform.position.y, transform.position.z), "sv_icon_dot13_pix16_gizmo", true);
    }
}
