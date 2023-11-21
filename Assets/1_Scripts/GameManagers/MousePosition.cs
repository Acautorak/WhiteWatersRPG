using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    private static MousePosition Instance;
    [SerializeField] private LayerMask mouseLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        /* Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mouseLayerMask);
        if (Input.GetMouseButtonDown(0)) HandleLeftMouseClick(); */


    }

    public static Vector2 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mouseLayerMask);
        return raycastHit.point;
    }

    private void HandleLeftMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mouseLayerMask);

        if (hit.collider != null)
        {
            Debug.Log("Object Clicked: " + hit.collider.gameObject.name);
            hit.collider.GetComponent<GridSystemVisualSingle>().Hide();

        }
    }
}
