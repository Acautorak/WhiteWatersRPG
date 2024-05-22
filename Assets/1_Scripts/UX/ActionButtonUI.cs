using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private TextMeshProUGUI cooldownTextMesh;

    private BaseAction baseAction;


    public void SetActionButton(BaseAction baseAction)
    {
        this.baseAction = baseAction;

        textMeshPro.text = baseAction.GetActionName().ToUpper();
        button.image.sprite = baseAction.GetActionImage();
        SetCoolDownVisuals();


        button.onClick.AddListener(() =>
        {
            UnifiedActionManager.Instance.SetSelectedAction(baseAction);
        });
    }

    public void SetCoolDownVisuals()
    {
        if (baseAction.IsOnCooldown())
        {
            cooldownImage.gameObject.SetActive(true);
            cooldownTextMesh.text = baseAction.GetCooldownCurrent().ToString().ToUpper();
        }
        else
        {
            cooldownImage.gameObject.SetActive(false);
        }
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnifiedActionManager.Instance.GetSelectedAction(); //UnitActionSystem
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
        SetCoolDownVisuals();
    }

    public void DestroyThisButton()
    {
        Destroy(gameObject);
    }
}
