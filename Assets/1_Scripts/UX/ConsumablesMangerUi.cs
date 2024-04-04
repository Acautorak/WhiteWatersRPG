using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesMangerUi : MonoBehaviour
{
    [SerializeField] private Button consumableButton, arrowButton, swap1, swap2, swap3, swap4;
    [SerializeField] private Sprite arrowSprite, reverseArrowSprite;

    private bool arrowUP = true;

    private void Start()
    {
        arrowButton.onClick.AddListener(() => OnArrowClick());
    }

    public void OnArrowClick()
    {
        //Close
        if (arrowUP)
        {
            arrowButton.image.sprite = arrowSprite;
        }

        //Open
        if (!arrowUP)
        {
            arrowButton.image.sprite = reverseArrowSprite;
        }

        arrowUP = !arrowUP;

    }
}
