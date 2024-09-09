using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private VisualEffect bloodVFX;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform bulletOriginTransform;
    [SerializeField] private Transform damagePopupTransform;

    [SerializeField] private Animator animator;
    private Unit thisUnit;
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        thisUnit = GetComponent<Unit>();

        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShot;
        }

        if (TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted += SwordAction_OnSwordActionCompleted;
        }
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDead += HealthSystem_OnDead;
    }


    public void PlayBlood()
    {
        bloodVFX.Play();
    }

    private void HealthSystem_OnDamaged(object sender, IntEventArgs e)
    {
        RotateForDamage();
        PlayBlood();
        DamagePopup damagePopup = Instantiate(damagePopupTransform, transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        damagePopup.SetUp(e.Value);
    }

    void RotateForDamage()
    {

        float rotationAmount = 15f;
        float rotationTime = 0.3f;
        // Store the original rotation
        Quaternion originalRotation = transform.rotation;

        // Rotate the object indicating damage
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, rotationAmount), rotationTime)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                // Rotate back to the original position after the damage indicator
                LeanTween.rotate(gameObject, originalRotation.eulerAngles, rotationTime)
                    .setEase(LeanTweenType.easeOutQuad);
            });
    }

    public void ShrinkAndReturnAnimation()
    {

        float shrinkScaleAmount = 1.1f;
        float animationDuration = 0.5f;
        // Store the original scale
        Vector3 originalScale = transform.localScale;

        // Shrink the GameObject
        LeanTween.scale(gameObject, originalScale * shrinkScaleAmount, animationDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                // Return to the original scale after shrinking
                LeanTween.scale(gameObject, originalScale, animationDuration)
                    .setEase(LeanTweenType.easeInQuad);
            });
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        PlayBlood();
        Debug.LogWarning("treba da umre");
        StartCoroutine("DieAfter");
        GridSystemVisual.Instance.UpdateGridVisual();
    }

    private IEnumerator DieAfter()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {

        StopCoroutine(DieAfter());
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        ShouldWalk(true);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        ShouldWalk(false);
    }

    private void ShootAction_OnShot(object sender, ShootAction.OnShootEventArgs e)
    {
        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, bulletOriginTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        bulletProjectile.Setup(e.targetUnit.GetWorldPosition());
    }

    public void ShouldWalk(bool param)
    {
        if(animator != null)
        animator.SetBool("ShouldWalk", param);
    }

    private void SwordAction_OnSwordActionStarted(object sender, EventArgs e)
    {

    }

    private void SwordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {

    }

}
