using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

// sto vise serializedfield, sto manje getcomponent
public class DamagePopup : MonoBehaviour
{
    private float disappearTimer;
    private double sinTime;
    TextMeshPro textMesh;
    private Color textColor;


    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        float moveYSpeed = 5f;
        sinTime += Time.deltaTime;
        transform.position += new Vector3(5 * (float)Math.Sin(5 * sinTime), moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 1f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }

    public void SetUp(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;

    }

}
