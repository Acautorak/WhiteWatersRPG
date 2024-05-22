using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonUIList;

    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnifiedActionManager.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSlecetedUnitChanged;
        UnifiedActionManager.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnifiedActionManager.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        UnifiedActionManager.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;

        //CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointsText();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            buttonTransform.GetComponent<ActionButtonUI>().DestroyThisButton();
        }

        actionButtonUIList.Clear();

        //UnitActionSystem
        Unit selectedUnit = UnifiedActionManager.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetActionButton(baseAction);

            actionButtonUIList.Add(actionButtonUI);
        }
    }

    private void UnitActionSystem_OnSlecetedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointsText();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
        if(UnifiedActionManager.Instance.IsPlayerTurn()) actionButtonContainerTransform.gameObject.SetActive(true);
        else actionButtonContainerTransform.gameObject.SetActive(false);
    }
    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPointsText()
    {
        //UnitActionSystem
        Unit selectedUnit = UnifiedActionManager.Instance.GetSelectedUnit();
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.SetCoolDownVisuals();
        }

       // actionPointsText.text = "Action points: " + selectedUnit.GetActionPoints();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }

}
