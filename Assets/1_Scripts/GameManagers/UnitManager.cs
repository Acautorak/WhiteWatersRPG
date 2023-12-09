using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public static UnitManager Instance { get; private set; }


    private List<Unit> unitList;
    private List<Unit> friendlyUnitList;
    private List<Unit> enemyUnitList;


    public List<Unit> turnOrderList;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        unitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();

        turnOrderList = new List<Unit>();
    }

    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
        StartCoroutine("AddAllUnitsToHashMap");
    }

    private IEnumerator AddAllUnitsToHashMap()
    {
        AddUnitsToHashMap();
        yield return new WaitForSeconds(1);

    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Add(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendlyUnitList.Add(unit);

        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        unitList.Remove(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitList.Remove(unit);
        }
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitList;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }


    public void AddUnitsToHashMap()
    {
        for (int i = 0; i < Math.Max(friendlyUnitList.Count, enemyUnitList.Count); i++)
        {
            if (friendlyUnitList[i] != null)
            {
                turnOrderList.Add(friendlyUnitList[i]);
                Debug.LogError("dodao sam friendly: " + friendlyUnitList[i].gameObject.name);
            }

            if (enemyUnitList[i] != null)
            {
                turnOrderList.Add(enemyUnitList[i]);
                Debug.LogWarning("dodao sam enemy: " + enemyUnitList[i].gameObject.name);

            }

        }
    }

}

