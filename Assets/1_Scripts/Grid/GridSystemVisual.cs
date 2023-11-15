using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }

    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        Yellow,
        RedSoft
    }

    [SerializeField] private Transform gridSystemVisualPrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Puko ti je GridSystemVisual");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {

        gridSystemVisualSingleArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < LevelGrid.Instance.GetHeight(); y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);

                Transform gridSystemVisualSingleTransform =
                    Instantiate(gridSystemVisualPrefab, LevelGrid.Instance.GetWorldPostion(gridPosition), Quaternion.identity);

                gridSystemVisualSingleArray[x, y] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }

        // UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        // LevelGrid.Instance.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;
        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < LevelGrid.Instance.GetHeight(); y++)
            {
                gridSystemVisualSingleArray[x, y].Hide();
            }
        }
    } public void ShowAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < LevelGrid.Instance.GetHeight(); y++)
            {
                gridSystemVisualSingleArray[x, y].Show();
            }
        }
    }

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, y);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(y);
                if (testDistance > range)
                {
                    continue;
                }

                gridPositionList.Add(testGridPosition);
            }
        }
        ShowGridPositionList(gridPositionList, gridVisualType);
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.y].Show();
        }
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }

        Debug.LogError("Could not find material for grid HELP??!: " + gridVisualType);
        return null;
    }

    private void UpdateGridVisual()
    {
        HideAllGridPositions();
        ShowAllGridPositions();
    }
}
