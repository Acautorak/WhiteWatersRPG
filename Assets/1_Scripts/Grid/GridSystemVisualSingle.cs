using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Show(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
        //spriteRenderer.material = material;
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        //spriteRenderer.enabled = false;
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
