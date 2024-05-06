using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.AddressableAssets.Settings;

public class FirstManager : PersistentMonoSingleton<FirstManager>
{
    public GameObject loadingScreen;
    public Image progressBar;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    float totalSceneProgress;
    public AddressableAssetGroup addressableAssetGroup;

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
