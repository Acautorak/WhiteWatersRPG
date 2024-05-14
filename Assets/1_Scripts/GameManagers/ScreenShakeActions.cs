using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    private void Start()
    {
        Notifier.Instance.Subscribe<AnyShootMessage>(ShootAction_OnAnyShoot);
        Notifier.Instance.Subscribe<AnyGrenadeExplodedMessage>(GrenadeProjectile_OnAnyGrenadeExploded);
    }

    private void OnDestroy()
    {
        Notifier.Instance.Unsubscribe<AnyShootMessage>(ShootAction_OnAnyShoot);
        Notifier.Instance.Unsubscribe<AnyGrenadeExplodedMessage>(GrenadeProjectile_OnAnyGrenadeExploded);
    }

    private void ShootAction_OnAnyShoot(AnyShootMessage message)
    {
        CameraShake.Instance.Shake();
    }

    private void GrenadeProjectile_OnAnyGrenadeExploded(AnyGrenadeExplodedMessage message)
    {
        CameraShake.Instance.Shake(5f);
    }

}
