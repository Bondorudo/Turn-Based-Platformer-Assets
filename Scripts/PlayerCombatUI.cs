using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombatUI : MonoBehaviour
{
    private GameState gameState;
    private PlayerAbilityHolder abilityHolder;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject playerMoveListContainer;


    /*
     * Show player move list when game is in player turn
     * in anyother state Hide move list
     * 
     * 
     */

    void Awake()
    {
        abilityHolder = player.GetComponent<PlayerAbilityHolder>();

        for (int i = 0; i < abilityHolder.ability.Count; i++)
        {
            int id = i;
            Debug.Log(abilityHolder.ability[i].abilityName);
            GameObject btn = Instantiate(button);
            btn.transform.SetParent(GameObject.Find("PlayerCommands").transform);
            btn.transform.localPosition = new Vector2(0, -40 * i);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = abilityHolder.ability[id].abilityName;
            btn.GetComponent<Button>().onClick.AddListener(() => abilityHolder.ability[id].Active(player));
            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30);
        }
    }

    private void Update()
    {
        if (gameState == GameState.PlayerTurn)
        {
            Show();
        }
        else
            Hide();
    }

    private void Show()
    {
        playerMoveListContainer.SetActive(true);
    }

    private void Hide()
    {
        playerMoveListContainer.SetActive(false);
    }
}
