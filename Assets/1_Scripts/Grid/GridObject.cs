using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridPosition gridPosition;
    private GridSystem gridSystem;
    //private List<Unit> unitList;

    public GridObject(GridPosition gridPosition, GridSystem gridSystem)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        //string unitString = "";
        //foreach (Unit unit in unitList)
            //unitString += unit + "\n";
        return gridPosition.ToString(); // + "\n" + unitString;
    }

    public void AddUnit(Unit unit)
    {
    }
}
