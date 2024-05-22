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
    }

    public static Vector2 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mouseLayerMask);
        Debug.LogError(raycastHit.point);
        return raycastHit.point;
    }

    private void HandleLeftMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mouseLayerMask);

        if (hit.collider != null)
        {
            Debug.Log("Object Clicked: " + hit.collider.gameObject.name);
            hit.collider.GetComponent<GridSystemVisualSingle>().Hide();

        }
    }

    public static Vector2 GetMouseWorldPosition2D()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = InputManager.Instance.GetMouseScreenPosition();

        // Set the Z-coordinate to the distance from the camera to the game world
        mousePosition.z = -Camera.main.transform.position.z;

        // Convert the mouse position from screen to world coordinates
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPosition;
    }
}