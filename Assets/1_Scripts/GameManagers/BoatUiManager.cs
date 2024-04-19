using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoatUiManager : MonoBehaviour
// Happy new year folk :) GUEESS WHOS BACKKK!!
{
    [SerializeField] RectTransform consumableShopWinow, consumableListContainer;
    [SerializeField] RectTransform mainMenu;
    [SerializeField] Button startButton, storeButton, optionsButton, xButton;
    [SerializeField] private BoatMove boatMove;
    [SerializeField] private TextMeshProUGUI goldText, gemsText;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            boatMove.StartMovement();
            IslandGenerator.Instance.SpawnRandomIsland();
            HideUiElemenets();
        });

        storeButton.onClick.AddListener(() =>
        {
            consumableShopWinow.gameObject.SetActive(true);
        }); 

        xButton.onClick.AddListener(() =>
        {
            HideShop();
        });

        PartyManager.Instance.OnGoldChanged += PartyManager_OnGoldChanged;
        goldText.text = PartyManager.Instance.gold.ToString();
        gemsText.text = PartyManager.Instance.gems.ToString();
    }

    public void HideUiElemenets()
    {
        float moveDuration = 1f;
        float offset = 1000f;
        LeanTween.moveY(mainMenu, offset, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            mainMenu.gameObject.SetActive(false);
        });

        if (!consumableShopWinow.gameObject.activeInHierarchy) return;
        LeanTween.moveY(consumableShopWinow, 2*offset, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            consumableShopWinow.gameObject.SetActive(false);
        });
    }

    private void HideShop()
    {
        consumableShopWinow.gameObject.SetActive(false);
    }

    private void PartyManager_OnGoldChanged(object sender, EventArgs e)
    {
        goldText.text = PartyManager.Instance.gold.ToString();
    }

    public void PopulateShopWindow()
    {
        
    }
}
