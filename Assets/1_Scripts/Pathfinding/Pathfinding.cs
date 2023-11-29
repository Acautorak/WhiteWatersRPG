using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance {get; private set;}
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private int width;
    private int height;
    private float cellSize;
    private GridSystem<PathNode> gridSystem;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError(" Puko ti je pathfinding Singleton");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        width = LevelGrid.Instance.GetWidth();
        height = LevelGrid.Instance.GetHeight();
        gridSystem = new GridSystem<PathNode>(width, height,
         LevelGrid.Instance.GetCellSize(), (GridSystem<PathNode> gameObject, GridPosition gridPosition) => new PathNode(gridPosition));
    }

    public List<GridPosition> FindPath(GridPosition startGridPostion, GridPosition endGridPosition)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startGridPostion);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);
        openList.Add(startNode);

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int y = 0; y < gridSystem.GetHeight(); y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }
        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startGridPostion, endGridPosition));
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if (currentNode == endNode)
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int tentativeGCost = currentNode.GetGCost()
                    + CalculateDistance(currentNode.GetGridPosition(), neighbourNode.GetGridPosition());

                if (tentativeGCost < neighbourNode.GetGCost())
                {
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(neighbourNode.GetGridPosition(), endNode.GetGridPosition()));
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // No path found
        return null;
    }

    public int CalculateDistance(GridPosition a, GridPosition b)
    {
        GridPosition gridPositionDistance = a - b;
        int distance = Mathf.Abs(gridPositionDistance.x) + Mathf.Abs(gridPositionDistance.y);
        int xDistance = Mathf.Abs(gridPositionDistance.x);
        int yDistance = Mathf.Abs(gridPositionDistance.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }
        return lowestFCostPathNode;
    }

    private PathNode GetNode(int x, int y)
    {
        return gridSystem.GetGridObject(new GridPosition(x, y));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridPosition = currentNode.GetGridPosition();

        if (gridPosition.x - 1 >= 0)
        {
            //left
            neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.y + 0));

            if (gridPosition.y - 1 >= 0)
            {
                //left down
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.y - 1));
            }

            if (gridPosition.y + 1 < height)
            {
                //left up
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.y + 1));
            }
        }

        if (gridPosition.x + 1 <= width)
        {
            //Right
            neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.y + 0));

            if (gridPosition.y - 1 >= 0)
            {
                //right down
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.y - 1));
            }

            if (gridPosition.y + 1 < height)
            {
                //right up
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.y + 1));
            }
        }

        if (gridPosition.y - 1 >= 0)
        {
            //Down
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.y - 1));
        }

        if (gridPosition.y + 1 < height)
        {
            //Up
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.y + 1));
        }


        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.GetCameFromPathNode() != null)
        {
            pathNodeList.Add(currentNode.GetCameFromPathNode());
            currentNode = currentNode.GetCameFromPathNode();
        }

        pathNodeList.Reverse();

        List<GridPosition> gridPositionList = new List<GridPosition>();
        foreach(PathNode pathNode in pathNodeList)
        {
            gridPositionList.Add(pathNode.GetGridPosition());
        }

        return gridPositionList;
    }

}
