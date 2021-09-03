using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : CombatUnits
{
    [SerializeField] private int baseActionCount = 1;

    public int actionCount { get; private set; }

    private GameState gameState;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        actionCount = baseActionCount;
    }

    /*
     * pelaaja valitsee tallennus pisteess‰ omat combat abilityt(?)
     * valitut abilityt lis‰t‰‰‰n listaan.
     * 
     * 
     *  Every movement tool gained in platforming can also be used as an attack in combat
     *  Different movement tools can be combined to create more powerful atttacks
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
    }

    public void ChangeActionCount()
    {
        actionCount--;
    }
}
