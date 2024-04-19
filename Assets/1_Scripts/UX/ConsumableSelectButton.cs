using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Image = UnityEngine.UI.Image;

public class ConsumableSelectButton : MonoBehaviour
{
    public string consumableID;
    public GameObject consumableDescriptionGO;
    [SerializeField] private Button ShowConsumableButton;
    [SerializeField] private TextMeshProUGUI itemName, itemCount;
}
