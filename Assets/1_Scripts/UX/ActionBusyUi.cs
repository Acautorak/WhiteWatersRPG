using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ovo realno ne treba da bude svoj monobehavior, generalno imas previse monobehaviora
// preporucujem kao prvi korak da spojis sve monobehavior-e koji su uvek zajedno na istom gameobjectu u jedan
// a kao drugi korak potencijalno da izdvajas logiku iz prevelikih skripti u komponovane pure C# skripte
public class ActionBusyUi : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        if(isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
