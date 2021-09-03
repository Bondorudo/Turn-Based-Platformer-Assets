using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombatUI : MonoBehaviour
{
    private PlayerAbilityHolder abilityHolder;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject playerMoveListContainer;


    void Awake()
    {
        abilityHolder = player.GetComponent<PlayerAbilityHolder>();

        for (int i = 0; i < abilityHolder.ability.Count; i++)
        {
            int id = i;
            GameObject btn = Instantiate(button);
            btn.transform.SetParent(GameObject.Find("PlayerCommands").transform);
            btn.transform.localPosition = new Vector2(0, -40 * i);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = abilityHolder.ability[id].abilityName;
            btn.GetComponent<Button>().onClick.AddListener(() => player.GetComponent<CombatPlayer>().SelectAbility(id));
            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30);
        }
    }

    private void Update()
    {
        if (CombatGameManager.instance.gameState == GameState.PlayerTurn)
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
