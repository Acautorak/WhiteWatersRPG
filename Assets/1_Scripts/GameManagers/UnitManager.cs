using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public static UnitManager Instance { get; private set; }


    [SerializeField]private List<Unit> unitList;
    private List<Unit> friendlyUnitList;
    private List<Unit> enemyUnitList;


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
    }

    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
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
                
                Debug.LogError("dodao sam friendly: " + friendlyUnitList[i].gameObject.name);
            }

            if (enemyUnitList[i] != null)
            {
                Debug.LogWarning("dodao sam enemy: " + enemyUnitList[i].gameObject.name);

            }

        }
    }

    public void SortAllUnitsByInitiative()
    {
        unitList.Sort((a, b) => b.unitStats.initiative.CompareTo(a.unitStats.initiative));
        friendlyUnitList.Sort((a, b) => b.unitStats.initiative.CompareTo(a.unitStats.initiative));
        enemyUnitList.Sort((a, b) => b.unitStats.initiative.CompareTo(a.unitStats.initiative));
        Debug.LogError("uspeo sam da isortiram unite po inicijativi");
    }

}

