using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class IslandGenerator : MonoBehaviour
{
    public static IslandGenerator Instance {get; private set;}
    [SerializeField] private IslandPrefabData levelIslands0;
    [SerializeField] private Transform islandSpawnPoint;
// Are you there little wolf?
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Puko ti je IslandGenerator");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
    }

    public void SpawnRandomIsland(IslandPrefabData levelData)
    {
        int randomIndex = Random.Range(0, levelData.islands.Length);
        GameObject islandGO = Instantiate(levelData.islands[randomIndex].gameObject, islandSpawnPoint.position, Quaternion.identity);
    }

    public void SpawnRandomIsland()
    {
        int randomIndex = Random.Range(0, levelIslands0.islands.Length);
        GameObject islandGO = Instantiate(levelIslands0.islands[randomIndex].gameObject, islandSpawnPoint.position, Quaternion.identity);
    }
}
