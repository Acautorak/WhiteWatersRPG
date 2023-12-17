using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UnifiedActionManager.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        //UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (UnifiedActionManager.Instance.GetSelectedUnit() == unit) // UnitActionSystem
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnDestroy()
    {
        //UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
        UnifiedActionManager.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
        
    }

}
