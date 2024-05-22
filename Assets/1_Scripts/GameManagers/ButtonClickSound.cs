using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class ButtonClickSound : MonoBehaviour
{
    public static ButtonClickSound Instance { get; private set; }
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip waterBackgroundSound;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.BOAT_SCENE)
        {
            Debug.Log("Oustio sam kurac!!");
            PlaySound(waterBackgroundSound, -20f);
        }
        else
        {
            Debug.Log("Ugasio sam kurac");
            // Ovde ga izbaci i ugasi
        }
    }

    private void Update()
    {
        if (InputManager.Instance.IsScreenTouchedOrClicked())
        {
            PlaySound(clickSound);
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }

    public void PlaySound(AudioClip audioClip, float volumeDB)
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(waterBackgroundSound, Camera.main.transform.position);
            //audioMixer.SetFloat("Volume", volumeDB);
        }
    }


}
