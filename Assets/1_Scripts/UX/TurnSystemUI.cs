using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisualGameObject;

    [SerializeField] private GameObject consumablesDropUpGameObject;

    private void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            UnifiedActionManager.Instance.NextTurn();
        });

        // TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UnifiedActionManager.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = "TURN: " + UnifiedActionManager.Instance.GetTurnNumber(); //TurnSystem
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }

    private void UpdateEnemyTurnVisual()
    {
        //enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
        enemyTurnVisualGameObject.SetActive(!UnifiedActionManager.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnButtonVisibility()
    {
        //endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
        endTurnButton.gameObject.SetActive(UnifiedActionManager.Instance.IsPlayerTurn());
        consumablesDropUpGameObject.SetActive(UnifiedActionManager.Instance.IsPlayerTurn());
    }
}
