using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;
    [SerializeField] private Sprite image;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
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

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();

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
            // return bestenemyAction[0]
            foreach(EnemyAiAction enemyAiAction in enemyAiActionList)
            {
                if(enemyAiAction.actionValue == enemyAiActionList[0].actionValue)
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
            // No possible action 
            return null;
        }
    }

    public abstract EnemyAiAction GetEnemyAiAction(GridPosition gridPosition);


}
