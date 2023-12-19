using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Island : MonoBehaviour
{
    [SerializeField] private float timeToArrive;
    public string islandSceneName;
    void Start()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.zero;

        LeanTween.move(gameObject, targetPos, timeToArrive).setEase(LeanTweenType.easeOutQuad).setOnComplete(()=> 
        {
            IslandGenerator.Instance.SpawnRandomIsland();
            SceneManager.LoadScene(islandSceneName);
            Destroy(gameObject);
        });
    }
}
