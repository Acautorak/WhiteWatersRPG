using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;
    private GridSystem<PathNode> gridSystem;

    private void Awake()
    {
        gridSystem = new GridSystem<PathNode>(LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight(),
         LevelGrid.Instance.GetCellSize(), (GridSystem<PathNode> gameObject, GridPosition gridPosition) => new PathNode(gridPosition));
    }

}
