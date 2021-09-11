using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombatUI : MonoBehaviour
{
    private AbilityHolder abilityHolder;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject playerMoveListContainer;

    [SerializeField] private GameObject affinitySprite;


    void Awake()
    {
        abilityHolder = player.GetComponent<AbilityHolder>();

        for (int i = 0; i < abilityHolder.ability.Count; i++)
        {
            int id = i;

            Transform parent = GameObject.Find("PlayerCommands").transform;

            Button btn = Instantiate(button).GetComponent<Button>();
            btn.transform.SetParent(parent);
            btn.transform.localPosition = new Vector3(100, 70 + (-50 * i));
            btn.GetComponentInChildren<TextMeshProUGUI>().text = abilityHolder.ability[id].abilityName;
            btn.onClick.AddListener(() => player.GetComponent<CombatPlayer>().SelectAbility(id));
            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 40);

            GameObject affSprite = Instantiate(affinitySprite);
            affSprite.transform.SetParent(parent);
            affSprite.transform.localPosition = new Vector3(-100, 70 + (-50 * i));
            affSprite.GetComponent<Image>().sprite = abilityHolder.ability[id].affinitySprite;

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
