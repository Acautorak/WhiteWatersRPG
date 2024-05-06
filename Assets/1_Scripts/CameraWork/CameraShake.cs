using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// opet singleton
// GetComponent koristi sto redje, i kad god koristis GetComponent, koristi i RequireComponent uz to
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance {get; private set;}
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("puko je screen shake");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
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
