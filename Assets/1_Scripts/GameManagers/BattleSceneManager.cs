using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance { get; private set; }

    public GridPosition gridPosition1, gridPosition2, gridPosition3, gridPosition4;
    private GridPosition agridPosition1, agridPosition2, agridPosition3, agridPosition4;


    private List<GridPosition> enemyGridPositionList;
    private List<GridPosition> aGridPositionList;

    private List<string> partyUnitStringList;

    public AssetLabelReference assetLabelReference;

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
        SetupStartingPositions();
        LoadStartingParty();
        LoadUnitPrefabs();
    }

    private void SetupStartingPositions()
    {
        gridPosition1 = new GridPosition(8, 1);
        gridPosition2 = new GridPosition(5, 3);
        gridPosition3 = new GridPosition(4, 4);
        gridPosition4 = new GridPosition(7, 6);

        agridPosition1 = new GridPosition(1, 1);
        agridPosition2 = new GridPosition(1, 3);
        agridPosition3 = new GridPosition(1, 4);
        agridPosition4 = new GridPosition(1, 6);

        enemyGridPositionList = new List<GridPosition>
        {
            gridPosition1, gridPosition2, gridPosition3, gridPosition4
        };

        aGridPositionList = new List<GridPosition>
        {
            agridPosition1, agridPosition2, agridPosition3,agridPosition4
        };
    }

    private void LoadStartingParty()
    {
        foreach (PartyUnit partyUnit in PartyManager.Instance.GetPartyUnitList())
        {
            LoadAssetByKey(partyUnit.unitID);
        }
    }

    public void LoadAssetByKey(string key)
    {
        Addressables.LoadAssetAsync<GameObject>(key).Completed += (asyncOperantioHandle) =>
        {
            if (asyncOperantioHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Instantiate(asyncOperantioHandle.Result);
            }
            else
            {
                Debug.Log("failed to load!");
            }
        };
    }

    void LoadUnitPrefabs()
    {
        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(assetLabelReference, null);

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
            for (int i = 0; i < numberOfUnitsToInstantiate; i++)
            {
                Instantiate(unitPrefabs[0], LevelGrid.Instance.GetWorldPosition(enemyGridPositionList[i]), Quaternion.identity);
                ShuffleUnitPrefabs(unitPrefabs);
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
