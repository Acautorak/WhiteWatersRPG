using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;


    private List<Vector2> targetPositionList;
    private int currentPositionIndex;
    private Quaternion originalRotation;

    [SerializeField] private int maxMoveDistance = 1;


    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float stoppingDistance = .1f;

        Vector2 targetPosition = targetPositionList[currentPositionIndex];
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(targetPosition, transform.position) > stoppingDistance)
        {

            float moveSpeed = 5f;

            transform.position += new Vector3(moveSpeed * Time.deltaTime * moveDirection.x, moveSpeed * Time.deltaTime * moveDirection.y, 0);
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= targetPositionList.Count)
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionComplete();
            }
        }

        // ako hoces da ne bude ease-in moras da sacuvas transform.forward kad se zaustavi i da ga koristis kao novu pocetnu vrednost

    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        List<GridPosition> pathGridPositionList = Pathfinding.Instance.FindPath(unit.GetGridPosition(), gridPosition, out int pathLength);

        currentPositionIndex = 0;
        targetPositionList = new List<Vector2>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            targetPositionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
        }

        OnStartMoving?.Invoke(this, EventArgs.Empty);
        ActionStart(onActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int y = -maxMoveDistance; y <= maxMoveDistance; y++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // same position where we at
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid pos already has unit
                    continue;
                }

                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!Pathfinding.Instance.HasPath(unitGridPosition, testGridPosition))
                {
                    continue;
                }

                int pathfindingDistanceMultiplier = 10;
                if (Pathfinding.Instance.GetPathLength(unitGridPosition, testGridPosition) > maxMoveDistance * pathfindingDistanceMultiplier)
                {
                    //Path Length is too long
                    continue;
                }


                validGridPositionList.Add(testGridPosition);
                //                Debug.LogWarning(testGridPosition.ToString());
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);
        if (targetCountAtGridPosition == 0)
        {
            return new EnemyAiAction
            {
                gridPosition = gridPosition,
                actionValue = 0

            };
        }
        else
        {
            return new EnemyAiAction
            {
                gridPosition = gridPosition,
                actionValue = 50 / targetCountAtGridPosition

            };
        }
    }
}
