using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        spriteRenderer = null;
    }

    public void Show(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
        //spriteRenderer.material = material; :D
    }

    public void Show()
    {
        if(spriteRenderer != null)
        spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        if(spriteRenderer != null)
        spriteRenderer.enabled = false;
    }
}
