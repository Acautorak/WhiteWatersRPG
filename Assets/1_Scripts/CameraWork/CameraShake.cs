using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            cinemachineImpulseSource.GenerateImpulse();
        }


    }
}