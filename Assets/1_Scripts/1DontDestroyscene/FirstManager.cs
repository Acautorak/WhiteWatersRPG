using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.AddressableAssets.Settings;

// bolje ime je npr SceneLoader
// ne reimplementiraj singleton, nego napravi i uvek koristi genericki singleton, dacu ti
// sva serijalizovana polja sakrivaj kao [SerializeField] private
// kad vec imas ovu klasu, pokusaj Unity-ev SceneManager da sakrijes da se u celom projektu samo koristi ovde
// i taman usput napravi ovu klasu da koristi UVEK tvoj SceneIndex, a nikad ime ili broj scene
public class FirstManager : MonoBehaviour
{
    public static FirstManager Instance { get; private set; } 

    public GameObject loadingScreen;
    public Image progressBar;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    float totalSceneProgress;
    public AddressableAssetGroup addressableAssetGroup;

    private void Awake()
    {
        DontDestroyOnLoad(loadingScreen);
        if (Instance != null)
        {
            Debug.LogError("Puko ti je FirstManager singleton and LO");
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
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.BOAT_SCENE, LoadSceneMode.Single));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadSceneCustom(SceneIndex sceneIndex)
    {
        loadingScreen.gameObject.SetActive(true);
       
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneIndex, LoadSceneMode.Single));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                progressBar.fillAmount = Mathf.RoundToInt(totalSceneProgress);
                yield return new WaitForSeconds(3);
            }
        }

        loadingScreen.SetActive(false);
        scenesLoading.Clear();
        StopCoroutine(GetSceneLoadProgress());
    }
}

public enum SceneIndex
{
    DONT_DESTROY_SCENE = 0,
    BOAT_SCENE = 1,
    NEW_TURN_BASED_SCENE = 2,
    LEVEL_PARK_SCENE = 3,
    
    
}
