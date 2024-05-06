using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static event EventHandler izbaci iz price :) event delegati se ispravno koriste samo na ne-staticki nacin
// ono sto zelis umesto toga je jedan Global Event Bus sistem, dacu ti
public class DestructibleCrate : MonoBehaviour
{

    public static event EventHandler OnAnyDestroy;

    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }
    public void Damage()
    {
        Destroy(gameObject);
        OnAnyDestroy?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
