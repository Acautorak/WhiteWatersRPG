using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float duration = 8f;
    public float startYOffset = 0.0011f;
    public float endYOffset = 0.0022f;
    [SerializeField] TextMeshPro textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        ShowDamagePopup("20");
    }

    public void ShowDamagePopup(string damageText)
    {
        textMesh.text = damageText;

        // Set the starting position of the damage popup below the desired position
        Vector3 startPos = transform.position;

        // Animate the damage popup to move up and fade out
        LeanTween.moveY(gameObject, startPos.y + endYOffset, duration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => Destroy(gameObject)); // Destroy the damage popup when animation completes

        LeanTween.alpha(gameObject, 0f, duration / 2f)
            .setDelay(duration / 2f);
    }
}
