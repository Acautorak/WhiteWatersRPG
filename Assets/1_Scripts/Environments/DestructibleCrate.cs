using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrate : MonoBehaviour
{
    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }
    public void Damage()
    {
        Destroy(gameObject);
        Notifier.Instance.Notify(new AnyDestructibleDestroyedMessage(this));
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
