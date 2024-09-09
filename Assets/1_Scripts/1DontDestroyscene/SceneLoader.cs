using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
public class SceneLoader : PersistentMonoSingleton<SceneLoader>
{
    public GameObject loadingScreen;
    public Image progressBar;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    float totalSceneProgress;
    float sceneTimer;

    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DontDestroy"))
        {
            LoadStartingScene();
        }
        DontDestroyOnLoad(loadingScreen);
    }

    public void LoadStartingScene()
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

                totalSceneProgress = totalSceneProgress / scenesLoading.Count * 100f;
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
    LEVEL_PIXEL_SCENE = 4,
    
    
}
