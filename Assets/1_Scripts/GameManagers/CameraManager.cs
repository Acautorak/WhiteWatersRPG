using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraGameObject;
    private void Start()
    {
        Notifier.Instance.Subscribe<AnyActionStartedMessage>(BaseAction_OnAnyActionStarted);
        Notifier.Instance.Subscribe<AnyActionCompletedMessage>(BaseAction_OnAnyActionCompleted);
    }
    private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGameObject.SetActive(false);
    }

    private void BaseAction_OnAnyActionStarted(AnyActionStartedMessage message)
    {
        switch (message.action)
        {
            case ShootAction shootAction:
                actionCameraGameObject.GetComponent<CinemachineCamera>().Follow = shootAction.GetTargetUnit().transform;
                ShowActionCamera();
                break;

            case MoveAction moveAction:
                actionCameraGameObject.GetComponent<CinemachineCamera>().Follow = moveAction.GetUnit().transform;
                ShowActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionCompleted(AnyActionCompletedMessage message)
    {
        HideActionCamera();
    }

    
    private void OnDestroy()
    {
        Notifier.Instance.Unsubscribe<AnyActionStartedMessage>(BaseAction_OnAnyActionStarted);
        Notifier.Instance.Unsubscribe<AnyActionCompletedMessage>(BaseAction_OnAnyActionCompleted);
    }
}
