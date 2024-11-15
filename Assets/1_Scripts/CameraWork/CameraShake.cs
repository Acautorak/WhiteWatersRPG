using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoSingleton<CameraShake>, ISelfInstantiatingMonoSingleton
{
    [SerializeField]private CinemachineImpulseSource cinemachineImpulseSource;

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cinemachineImpulseSource.GenerateImpulse();
        }


    }

    public void Shake(float intensity = 1f)
    {
        cinemachineImpulseSource.GenerateImpulse(intensity);
    }
}
