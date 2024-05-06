using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// action je malo neprirodan koncept za monobehaviour
// zgodniji bi bili za koriscenje da je kompletna hijerarhija cisti c#
// takodje uvek ispadne bolja prica kasnije kad koristis interfejse umesto klasnog nasledjivanja, bude cistije i lakse
// u ovom slucaju bi imao neku obicnu klasu ActionExecutor koji u sebi ima IAction kao strategy pattern
// i cilj ti je svugde u kodu gde sad koristis BaseAction da umesto toga koristis IAction
public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;
    [SerializeField] private Sprite image;
    [SerializeField] private int cooldownMax;
    private int cooldownCurrent;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    protected virtual void Start()
    {
        UnifiedActionManager.Instance.OnRoundChanged += UnifiedActionManager_OnRoundChanged;
    }


    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointCost()
    {
        return 1;
    }

    public virtual int GetCooldownCurrent()
    {
        return cooldownCurrent;
    }

    public virtual bool IsOnCooldown()
    {
        if (cooldownMax == 0) return false;

        if (cooldownCurrent == 0) return false;

        

        else return true;
    }

    protected void ReduceCooldown()
    {
        if (cooldownCurrent == 0) return;
        cooldownCurrent--;
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        cooldownCurrent = cooldownMax;
        // ovde gledaj da fixas CDs
        
        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
        cooldownCurrent = cooldownMax;
        

        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public Sprite GetActionImage()
    {
        return image;
    }

    public EnemyAiAction GetBestEnemyAiAction()
    {
        List<EnemyAiAction> enemyAiActionList = new List<EnemyAiAction>();
        List<EnemyAiAction> bestEnemyAiActionList = new List<EnemyAiAction>();

        List<GridPosition> validActionGridPositionList = GetValidActionGridPositionList();

        foreach (GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAiAction enemyAiAction = GetEnemyAiAction(gridPosition);
            enemyAiActionList.Add(enemyAiAction);
        }

        if (enemyAiActionList.Count > 0)
        {
            enemyAiActionList.Sort((EnemyAiAction a, EnemyAiAction b) => b.actionValue - a.actionValue);
            // return bestenemyAction[0] :) #
            foreach (EnemyAiAction enemyAiAction in enemyAiActionList)
            {
                if (enemyAiAction.actionValue == enemyAiActionList[0].actionValue)
                {
                    bestEnemyAiActionList.Add(enemyAiAction);
                }
            }
            if (bestEnemyAiActionList.Count > 0)
            {
                return bestEnemyAiActionList[UnityEngine.Random.Range(0, bestEnemyAiActionList.Count - 1)];
            }
            else return null;
        }
        else
        {
            // No possible action guess who's back!? :)
            return null;
        }
    }

    public abstract EnemyAiAction GetEnemyAiAction(GridPosition gridPosition);

    protected virtual void UnifiedActionManager_OnRoundChanged(object sender, EventArgs e)
    {
        ReduceCooldown();
    }


}
