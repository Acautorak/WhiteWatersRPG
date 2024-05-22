using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Image = UnityEngine.UI.Image;

public class ConsumableSelectButton : MonoBehaviour
{
    public string consumableID;
    public GameObject consumableDescriptionGO;
    [SerializeField] private Button showConsumableButton;
    [SerializeField] private TextMeshProUGUI itemName, itemCount;

    private void Start()
    {
        showConsumableButton.onClick.AddListener(() => ShowConsumable());
    }

    public void SetItemName(string name)
    {
        itemName.text = name;
    }

    public void SetItemCount(int count)
    {
        itemCount.text = count.ToString();
    }

    public void ShowConsumable()
    {
        
    }

    public void Setup(string name, int count)
    {
        SetItemCount(count);
        SetItemName(name);
        ShowConsumable();
    }
}
