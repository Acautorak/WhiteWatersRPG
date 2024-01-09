using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstManager : MonoBehaviour
{
    public static FirstManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Puko ti je FirstManager singleton");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DontDestroy"))
        {
            LoadSceneCustom();
        }
    }

    public void LoadSceneCustom()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndex.DONT_DESTROY_SCENE);
        SceneManager.LoadSceneAsync((int)SceneIndex.BOAT_SCENE, LoadSceneMode.Single);
    }
}

public enum SceneIndex
{
    DONT_DESTROY_SCENE = 0,
    BOAT_SCENE = 1,
    NEW_TURN_BASED_SCENE = 2
}
