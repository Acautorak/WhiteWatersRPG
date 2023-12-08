using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    [SerializeField] private Transform[] path0, path1, path2;
    [SerializeField] private int pathIndex = 0;
    [SerializeField] private float rotationTime = 5f;
    [SerializeField] private GameObject boat;

    private void Start()
    {
        StartMovement();
    }
    public void StartMovement()
    {
        MoveToNextWaypoint(path0);
    }

    private void MoveToNextWaypoint(Transform[] path)
    {
        if (pathIndex < path.Length)
        {
            Vector2 targetPosition = path[pathIndex].transform.position;
            //LeanTween.move(boat, targetPosition, moveTime).setEase(LeanTweenType.pingPong).setOnComplete(() => { pathIndex++; MoveToNextWaypoint(path); });
            LeanTween.rotateZ(boat, CalculateTargetRotation(targetPosition), rotationTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => 
            { pathIndex++; 
            StartMovement(); });
        }
        else
        {
            pathIndex = 0;
            Debug.LogWarning("dosli smo do kraja");
        }
    }

    private float CalculateTargetRotation(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)boat.transform.position).normalized;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }
}
