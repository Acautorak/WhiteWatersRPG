using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BoatUiManager : MonoBehaviour
{
    [SerializeField] RectTransform mainMenu;
    [SerializeField] Button startButton, storeButton, optionsButton;
    [SerializeField] private BoatMove boatMove;

    private void Start()
    {
        startButton.onClick.AddListener(()=> 
        {
            boatMove.StartMovement();
            IslandGenerator.Instance.SpawnRandomIsland();
            HideUiElemenets();
        });
    }

    public void HideUiElemenets()
    {
        float moveDuration = 1f;
        float offset = -1000f;
        LeanTween.moveX(mainMenu, offset, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(()=>
        {
        mainMenu.gameObject.SetActive(false);
        });
    }
}
