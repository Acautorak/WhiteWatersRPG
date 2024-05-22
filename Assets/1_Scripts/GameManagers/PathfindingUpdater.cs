using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    void Start()
    {
        Notifier.Instance.Subscribe<AnyDestructibleDestroyedMessage>(DestructibleCrate_OnAnyDestroy);
    }

    void OnDestroy()
    {
        Notifier.Instance.Unsubscribe<AnyDestructibleDestroyedMessage>(DestructibleCrate_OnAnyDestroy);
    }

    private void DestructibleCrate_OnAnyDestroy(AnyDestructibleDestroyedMessage anyDestructibleDestroyedMessage)
    {
        DestructibleCrate destructibleCrate = anyDestructibleDestroyedMessage.destructibleCrate;
        Pathfinding.Instance.SetIsWalkableGridPosition(destructibleCrate.GetGridPosition(), true);
    }
}
