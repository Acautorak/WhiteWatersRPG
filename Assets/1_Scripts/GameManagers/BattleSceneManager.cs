using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance { get; private set; }  

    public GridPosition gridPosition1, gridPosition2, gridPosition3, gridPosition4;

    private List<GridPosition> enemyGridPositionList;

    public string unitPrefabsAddressableKey = "UnitPrefabs";
    public int numberOfUnitsToInstantiate = 4;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Puko ti je BattleSceneManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gridPosition1 = new GridPosition(5 - 1, 1);
        gridPosition2 = new GridPosition(5 - 1, 3);
        gridPosition3 = new GridPosition(5 - 1, 4);
        gridPosition4 = new GridPosition(5 - 1, 6);

        enemyGridPositionList = new List<GridPosition>
        {
            gridPosition1, gridPosition2, gridPosition3, gridPosition4
        };

        LoadUnitPrefabs();
    }

    void LoadUnitPrefabs()
    {
        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(unitPrefabsAddressableKey, null);

        handle.Completed += OnUnitPrefabsLoaded;
    }

    void OnUnitPrefabsLoaded(AsyncOperationHandle<IList<GameObject>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            IList<GameObject> unitPrefabs = handle.Result;

            // Shuffle unitPrefabs if needed
            ShuffleUnitPrefabs(unitPrefabs);

            // Instantiate the first numberOfUnitsToInstantiate units
            for (int i = 0; i < Mathf.Min(numberOfUnitsToInstantiate, unitPrefabs.Count); i++)
            {
                Instantiate(unitPrefabs[i], LevelGrid.Instance.GetWorldPosition(enemyGridPositionList[i]), Quaternion.identity);
            }

            // Unload unit prefabs when they are no longer needed
            UnloadUnitPrefabs(unitPrefabs);
        }
        else
        {
            Debug.LogError($"Failed to load unit prefabs: {handle.Status}");
        }
    }

    void ShuffleUnitPrefabs(IList<GameObject> unitPrefabs)
    {
        // Shuffle unitPrefabs using Fisher-Yates algorithm
        for (int i = unitPrefabs.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = unitPrefabs[i];
            unitPrefabs[i] = unitPrefabs[randomIndex];
            unitPrefabs[randomIndex] = temp;
        }
    }

    void UnloadUnitPrefabs(IList<GameObject> unitPrefabs)
    {
        foreach (GameObject prefab in unitPrefabs)
        {
            Addressables.ReleaseInstance(prefab);
        }
    }


}
