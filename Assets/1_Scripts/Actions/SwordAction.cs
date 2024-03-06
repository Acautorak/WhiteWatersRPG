using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : BaseAction
{

    public event EventHandler OnSwordActionStarted;
    public event EventHandler OnSwordActionCompleted;

    private int maxSwordDistance = 1;
    private State state;
    private float stateTimer;

    private float rotateSpeed = 10f;

    private Unit targetUnit;
    private Quaternion originalRotation;

    private enum State
    {
        SwingingSwordBeforeHit,
        SwingingSwordAfterHit

    }

    protected override void Start()
    {
        base.Start();
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.SwingingSwordBeforeHit:
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;

                transform.up = Vector2.Lerp(transform.up, aimDir, rotateSpeed * Time.deltaTime);
                break;

            case State.SwingingSwordAfterHit:
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case State.SwingingSwordBeforeHit:
                state = State.SwingingSwordAfterHit;
                float afterHitStateTime = 0.5f;
                stateTimer = afterHitStateTime;
                targetUnit.Damage(DamageManager.Instance.CalculateMeleDamage(unit, targetUnit, 50));
                break;

            case State.SwingingSwordAfterHit:
                float rotationTime = 0.3f;
                LeanTween.rotate(gameObject, originalRotation.eulerAngles, rotationTime)
                    .setEase(LeanTweenType.easeOutQuad);
                
                OnSwordActionCompleted?.Invoke(this, EventArgs.Empty);
                ActionComplete();
                break;
        }

    }
    public override string GetActionName()
    {
        return "Sword";
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition gridPosition)
    {
        return new EnemyAiAction
        {
            gridPosition = gridPosition,
            actionValue = 200
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();


        for (int x = -maxSwordDistance; x <= maxSwordDistance; x++)
        {
            for (int y = -maxSwordDistance; y <= maxSwordDistance; y++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;



                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid pos is empty, no unit
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    // both units on same team
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        state = State.SwingingSwordBeforeHit;
        float beforeHitStateTime = 0.7f;
        stateTimer = beforeHitStateTime;

        OnSwordActionStarted?.Invoke(this, EventArgs.Empty);
        ActionStart(onActionComplete);
    }

    public int GetMaxSwordDistance()
    {
        return maxSwordDistance;
    }
}
