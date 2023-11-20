using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public void Show()
    {
        spriteRenderer.enabled = true;  
        //spriteRenderer.material = material;
    }

    public void Hide()
    {
        //spriteRenderer.enabled = false;
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
