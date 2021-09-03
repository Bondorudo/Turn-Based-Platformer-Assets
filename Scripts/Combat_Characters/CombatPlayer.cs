using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : CombatUnits
{
    [SerializeField] private int baseActionCount = 1;

    public int actionCount { get; private set; }

    private GameState gameState;

    public GameObject targetedEnemy;

    public Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        actionCount = baseActionCount;
        anim = GetComponent<Animator>();
    }

    /*
     * pelaaja valitsee tallennus pisteess‰ omat combat abilityt(?)
     * valitut abilityt lis‰t‰‰‰n listaan.
     * 
     * 
     *  Every movement tool gained in platforming can also be used as an attack in combat
     *  Different movement tools can be combined to create more powerful atttacks
     *  
     *  
     *  TODO: player valitsee abilityn ja sen j‰lkeen targettaa vihollista!!!!
     *  
     *  
     *  
     */

    // Update is called once per frame
    void Update()
    {
        if (actionCount <= 0)
        {
            gameState = GameState.EnemyTurn;
        }

        if (gameState == GameState.EnemyTurn)
        {
            actionCount = baseActionCount;
        }

        TargetEnemy();
    }

    public void ChangeActionCount()
    {
        actionCount--;
    }

    private void TargetEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButton(0))
        {
            if (!hit)
                return;

            if (hit.transform.tag == "Enemy")
            {
                targetedEnemy = hit.transform.gameObject;
                Debug.Log("Target changed");
            }
        }
    }
}
