using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonClickSound : MonoBehaviour
{
    public static ButtonClickSound Instance { get; private set; }
    [SerializeField] private AudioClip clickSound;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
    }

}
