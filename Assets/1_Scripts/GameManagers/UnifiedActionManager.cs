using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UnifiedActionManager : MonoBehaviour
{
    public static UnifiedActionManager Instance { get; private set; }

    // -------UnitAction---------
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit selectedUnit;
    //[SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private BaseAction selectedAction;

    private bool isBusy;

    // ---------Turns---------------
    public event EventHandler OnTurnChanged;
    public event EventHandler OnRoundChanged;

    private int turnNumber = 0;
    private int currentUnitIndex = 0;
    private int turnsCompleted = 0;
    private int roundNumber = 0;
    [SerializeField] private bool isPlayerTurn = true;

    //--------EnemyAi------------

    private State state;
    private float timer;
    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }

    //-----------UnitManager-----------

    [SerializeField] private List<Unit> unitList;
    private List<Unit> friendlyUnitList;
    private List<Unit> enemyUnitList;

    // --------------------------------

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Puko ti je UnifiedActionManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        unitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();

        state = State.WaitingForEnemyTurn;
    }

    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
        OnTurnChanged += TurnSystem_OnTurnChanged;
        OnTurnChanged += UnitActionSystem_OnTurnChanged;
        unitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
    }

    private void OnDestroy()
    {
        Unit.OnAnyUnitSpawned -= Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead -= Unit_OnAnyUnitDead;
        OnTurnChanged -= TurnSystem_OnTurnChanged;
        OnTurnChanged -= UnitActionSystem_OnTurnChanged;
    }

    private void Update()
    {
        if (IsPlayerTurn())
        {
            if (isBusy)
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            HandleSelectedAction();
        }

        else if (!IsPlayerTurn())
        {


            switch (state)
            {
                case State.WaitingForEnemyTurn:
                    break;
                case State.TakingTurn:
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                        if (TryTakeEnemyAIAction(selectedUnit, SetStateTakingTurn))
                        {
                            state = State.Busy;
                        }
                        else
                        {
                            // No more enemies have actions they can take, end enemy turn
                            EnemyNextTurn();
                        }
                    }
                    break;
                case State.Busy:
                    break;
            }
        }
    }

    #region ------------UnitActionSystemLogic--------------



    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MousePosition.GetMouseWorldPosition2D());
            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return;
            }

            if (!selectedUnit.TrySpendActionPoints(selectedAction))
            {
                return;
            }
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);

            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }
    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    public void SetupSelectedUnit()
    {
        selectedUnit = unitList[currentUnitIndex];
        if (selectedUnit.IsEnemy()) isPlayerTurn = false;
        else isPlayerTurn = true;
        SetSelectedUnit(selectedUnit);
    }


    private void UnitActionSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if (selectedUnit.GetHealthNormalized() <= 0)
        {
            List<Unit> friendlyUnitList = GetFriendlyUnitList();

            if (friendlyUnitList.Count > 0)
            {
                SetSelectedUnit(GetUnitList()[0]);
            }
            else Debug.LogWarning("Game Over");
        }
    }
    #endregion

    #region ----------EnemyAi---------------


    private void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        EnemyAiAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;

        foreach (BaseAction baseAction in enemyUnit.GetBaseActionArray())
        {
            if (!enemyUnit.CanSpendActionPointsToTakeAction(baseAction))
            {
                // Enemy cannot afford this action
                continue;
            }

            if (bestEnemyAIAction == null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAiAction();
                bestBaseAction = baseAction;
            }
            else
            {
                EnemyAiAction testEnemyAIAction = baseAction.GetBestEnemyAiAction();
                if (testEnemyAIAction != null && testEnemyAIAction.actionValue > bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = testEnemyAIAction;
                    bestBaseAction = baseAction;
                }
            }

        }

        if (bestEnemyAIAction != null && enemyUnit.TrySpendActionPoints(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAIActionComplete);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if (!IsPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }
    }

    #endregion

    #region ---------TurnSystemLogic--------

    private void StartRound()
    {
        turnsCompleted = 0;
        //SortAllUnitsByInitiative();
        StartNextTurn();
    }

    private void StartNextTurn()
    {
        if(turnsCompleted == unitList.Count)
        {
            EndRound();
        }
        else
        {
            SetupSelectedUnit();
            currentUnitIndex = (currentUnitIndex + 1) % unitList.Count;
            turnsCompleted++;
        }
    }

    private void EndRound()
    {
        //Logika za kraj runde
        StartRound();
    }

    public void NextTurn()
    {
        turnNumber++;
        if (turnNumber < 0 || turnNumber > unitList.Count)
        {
            turnNumber = 0;
            NextRound();
        }

        isPlayerTurn = false;
        //isPlayerTurn = !isPlayerTurn;
        //SetupSelectedUnit();
        StartNextTurn();
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public void EnemyNextTurn()
    {
        turnNumber++;
        if (turnNumber < 0 || turnNumber > unitList.Count - 1)
        {
            turnNumber = 0;
            NextRound();
        }
        isPlayerTurn = true;
        StartNextTurn();
        //SetupSelectedUnit();

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public void NextRound()
    {
        roundNumber++;
        OnRoundChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public int GetRoundNumber()
    {
        return roundNumber;
    }
    #endregion

    #region -------UnitManagerLogic----------
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

        if(unitList.Count == GetTotalUnitCount())
        {
            StartRound();
        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
            if (enemyUnitList.Count == 0)
            {
                WinSceneChange();
            }
            //SortAllUnitsByInitiative();
            //SetupSelectedUnit();

        }
        else
        {
            friendlyUnitList.Remove(unit);
            if (friendlyUnitList.Count == 0)
            {
                LoseSceneChange();
            }
            //SortAllUnitsByInitiative();
            //SetupSelectedUnit();
        }

        unitList.Remove(unit);
        if(currentUnitIndex >= unitList.Count)
        {
            currentUnitIndex = 0;
        }

    }

    private int GetTotalUnitCount()
    {
        // OVDE CE DA RADI INITIALIZATION MANAGER
        return 5;
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

    public void SortAllUnitsByInitiative()
    {
        if(roundNumber == 0)
        {
            Debug.Log("Smanjujem");
            currentUnitIndex --;
            Debug.LogWarning(currentUnitIndex);
            roundNumber++;
        }
        unitList.Sort((a, b) => b.unitStats.initiative.CompareTo(a.unitStats.initiative));
        friendlyUnitList.Sort((a, b) => b.unitStats.initiative.CompareTo(a.unitStats.initiative));
        enemyUnitList.Sort((a, b) => b.unitStats.initiative.CompareTo(a.unitStats.initiative));
        Debug.LogError("uspeo sam da isortiram unite po inicijativi");
        Debug.Log(currentUnitIndex);

    }
    #endregion

    public void WinSceneChange()
    {
        RewardsManagerUi.Instance.ShowRewardsTab();
    }

    public void LoseSceneChange()
    {
        FirstManager.Instance.LoadSceneCustom(SceneIndex.BOAT_SCENE);
    }
}
