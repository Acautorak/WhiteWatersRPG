using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesMangerUi : MonoBehaviour
{
    [SerializeField] private Button consumableButton, arrowButton, swap1, swap2, swap3, swap4;
    [SerializeField] private Sprite arrowSprite, reverseArrowSprite;
    [SerializeField] private GameObject swapContainer;

    private int currentSelectedConsumable;

    private bool arrowUP = true;

    private void Start()
    {
        arrowButton.onClick.AddListener(() => OnArrowClick());
        consumableButton.onClick.AddListener(() => OnConsumeClicked());
        swap1.onClick.AddListener(() => OnSwapClick1());
        swap2.onClick.AddListener(() => OnSwapClick2());
        swap3.onClick.AddListener(() => OnSwapClick3());
        swap4.onClick.AddListener(() => OnSwapClick4());
    }

    public void OnArrowClick()
    {
        //Close
        if (arrowUP)
        {
            arrowButton.image.sprite = arrowSprite;
            swapContainer.SetActive(false);
        }

        //Open
        if (!arrowUP)
        {
            arrowButton.image.sprite = reverseArrowSprite;
            swapContainer.SetActive(true);
        }

        arrowUP = !arrowUP;

    }

    public void OnConsumeClicked()
    {
        //Consume equippedConsumable[currentSelectedConsumable]
        Debug.Log("Trenutno je " + currentSelectedConsumable);

    }
    public void OnSwapClick1()
    {
        consumableButton.image.sprite = swap1.image.sprite;
        currentSelectedConsumable = 0;
    }
    public void OnSwapClick2()
    {
        consumableButton.image.sprite = swap2.image.sprite;
        currentSelectedConsumable = 1;
    }
    public void OnSwapClick3()
    {
        consumableButton.image.sprite = swap3.image.sprite;
        currentSelectedConsumable = 2;
    }
    public void OnSwapClick4()
    {
        consumableButton.image.sprite = swap4.image.sprite;
        currentSelectedConsumable = 3;
    }
}
