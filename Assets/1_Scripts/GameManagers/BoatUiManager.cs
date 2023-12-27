using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BoatUiManager : MonoBehaviour
// Happy new year folk :)
{
    [SerializeField] RectTransform theSalon;
    [SerializeField] RectTransform mainMenu;
    [SerializeField] Button startButton, storeButton, optionsButton, hideUiButton, xButton;
    [SerializeField] private BoatMove boatMove;

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
            theSalon.gameObject.SetActive(true);
        });

        hideUiButton.onClick.AddListener(() =>
        {
            HideUiElemenets();
        });

        xButton.onClick.AddListener(() =>
        {
            HideShop();
        });
    }

    public void HideUiElemenets()
    {
        float moveDuration = 1f;
        float offset = -1000f;
        LeanTween.moveX(mainMenu, offset, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            mainMenu.gameObject.SetActive(false);
        });

        if (!theSalon.gameObject.activeInHierarchy) return;
        LeanTween.moveX(theSalon, 2*offset, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            theSalon.gameObject.SetActive(false);
        });
    }

    private void HideShop()
    {
        theSalon.gameObject.SetActive(false);
    }
}
