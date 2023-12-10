using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private VisualEffect bloodVFX;
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    public void PlayBlood()
    {
        bloodVFX.Play();
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        RotateForDamage();
        PlayBlood();
    }

    void RotateForDamage()
    {

        float rotationAmount = 15f;
        float rotationTime = 0.3f;
        // Store the original rotation
        Quaternion originalRotation = transform.rotation;

        // Rotate the object indicating damage
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, rotationAmount), rotationTime)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                // Rotate back to the original position after the damage indicator
                LeanTween.rotate(gameObject, originalRotation.eulerAngles, rotationTime)
                    .setEase(LeanTweenType.easeOutQuad);
            });
    }

    public void ShrinkAndReturnAnimation()
    {

        float shrinkScaleAmount = 0.95f;
        float animationDuration = 0.5f;
        // Store the original scale
        Vector3 originalScale = transform.localScale;

        // Shrink the GameObject
        LeanTween.scale(gameObject, originalScale * shrinkScaleAmount, animationDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                // Return to the original scale after shrinking
                LeanTween.scale(gameObject, originalScale, animationDuration)
                    .setEase(LeanTweenType.easeInQuad);
            });
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        PlayBlood();
        Debug.LogWarning("treba da umre");
        StartCoroutine("DieAfter");

    }

    private IEnumerator DieAfter()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.LogError("ZAUSTAVLJAM KORUTINE");
        StopAllCoroutines();
    }
}
