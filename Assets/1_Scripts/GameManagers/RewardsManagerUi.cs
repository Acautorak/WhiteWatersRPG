using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardsManagerUi : MonoBehaviour
{
    public static RewardsManagerUi Instance { get; private set; }
    [SerializeField] GameObject rewardsCanvasPrefab;
    [SerializeField] Button closeRewardsButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Puko je RewardsManagerUi");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        if (rewardsCanvasPrefab.activeInHierarchy)
        {
            rewardsCanvasPrefab.SetActive(false);
        }

        closeRewardsButton.onClick.AddListener(() =>
        {
            rewardsCanvasPrefab.SetActive(false);
            //SceneLoader.Instance.LoadSceneCustom(SceneIndex.BOAT_SCENE);
        });

    }

    public void ShowRewardsTab()
    {
        rewardsCanvasPrefab.SetActive(true);
        LeanTween.value(rewardsCanvasPrefab.gameObject, UpdateAlpha, 0, 1f, 1.1f);
    }




    private void UpdateAlpha(float alpha)
    {
        Color newColor = rewardsCanvasPrefab.GetComponent<Image>().color;
        newColor.a = alpha;
        rewardsCanvasPrefab.GetComponent<Image>().color = newColor;
    }





}
