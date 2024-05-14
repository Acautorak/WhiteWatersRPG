using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{

    [SerializeField] private Transform explodeVfxPrefab;
    [SerializeField] private TrailRenderer trailRenderer;
    private Vector3 targetPosition;
    private Action onGrenadeBehaviourComplete;


    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float moveSpeed = 15f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float reachTargetDistance = 0.2f;
        if (Vector3.Distance(transform.position, targetPosition) < reachTargetDistance)
        {
            float damageRadius = 11f;
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(targetPosition, damageRadius);
            foreach (Collider2D collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(100);
                }

                if (collider.TryGetComponent<DestructibleCrate>(out DestructibleCrate destructibleCrate))
                {
                    destructibleCrate.Damage();
                }

            }
            Notifier.Instance.Notify(new AnyGrenadeExplodedMessage());

            trailRenderer.transform.parent = null;
            Instantiate(explodeVfxPrefab, targetPosition, Quaternion.identity);
            Destroy(gameObject);

            onGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
    }
}
