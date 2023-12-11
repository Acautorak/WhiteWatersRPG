using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVFXTransform;
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float distanceBeforeMoveing = Vector3.Distance(transform.position, targetPosition);
        float moveSpeed = 150f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceBeforeMoveing < distanceAfterMoving)
        {
            transform.position = targetPosition;

            trailRenderer.transform.parent = null;
            Destroy(gameObject);

            Instantiate(bulletHitVFXTransform, targetPosition, Quaternion.identity);
        }

    }
}
