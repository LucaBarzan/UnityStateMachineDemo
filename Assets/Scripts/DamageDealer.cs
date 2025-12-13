using System;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public Action<bool, DamageableInfo> OnDealDamageResult;

    public DamageSO Damage;
    public Transform AttackerTransform;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private LayerMaskSO obstacleLayers;

    private void Awake()
    {
        if (AttackerTransform == null)
            AttackerTransform = transform;
    }

    public void OnCollisionDetected(Collider2D other) => TryDealDamage(other);

    private void TryDealDamage(Collider2D other)
    {
        if (!enabled || other == null || Damage == null)
            return;

        var damageableInfo = new DamageableInfo(other.gameObject, other.GetComponent<Health>());

        // Trying to hit something that is not damageable
        if (damageableInfo.Health == null)
        {
            OnDealDamageResult?.Invoke(false, damageableInfo);
            return;
        }

        var hitPoint = other.ClosestPoint(transform.position);
        var direction = hitPoint - (Vector2)AttackerTransform.position;

        if (IsObstacleBlocking(other, direction))
        {
            OnDealDamageResult?.Invoke(false, damageableInfo);
            return;
        }

        var attackInfo = new AttackInfo(
            Damage,
            hitPoint,
            direction,
            AttackerTransform.gameObject
        );

        bool success = damageableInfo.Health.TakeDamage(attackInfo);

        OnDealDamageResult?.Invoke(success, damageableInfo);
    }

    // Check if there is any obstacle in between the hitbox and the other object
    private bool IsObstacleBlocking(Collider2D otherCollider, Vector2 direction)
    {
        if (!Damage.CheckForObstacles)
            return false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(AttackerTransform.position, direction.normalized, direction.magnitude, obstacleLayers.LayerMask);

        bool foundObstacle = false;
        Vector2 hitPoint = Vector2.zero;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != myCollider && hits[i].collider != otherCollider)
            {
                hitPoint = hits[i].point;
                foundObstacle = true;
                break;
            }
        }

        return foundObstacle;
    }
}