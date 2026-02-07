using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Detects surface contacts (ground, walls, and ceiling) using BoxCast queries.
/// Attach this to a GameObject with a BoxCollider2D to sense nearby surfaces.
/// </summary>
public class SurfaceContactSensor : MonoBehaviour
{
    public event Action<RaycastHit2D> OnGroundHitChanged;
    public event Action<RaycastHit2D> OnCeilingHitChanged;
    public event Action<RaycastHit2D> OnRightWallHitChanged;
    public event Action<RaycastHit2D> OnLeftWallHitChanged;

    [Header("References")]
    [SerializeField] private CapsuleCollider2D capsuleCollider;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask contactLayerMask;

    [Header("Ground Detection")]
    [SerializeField] private bool checkGround = true;
    [SerializeField] private Vector2 groundCheckSize;

    [Header("Wall Detection")]
    [SerializeField] private bool checkRightWall = false;
    [SerializeField] private bool checkLeftWall = false;
    [SerializeField] private Vector2 wallCheckSize;

    [Header("Ceiling Detection")]
    [SerializeField] private bool checkCeiling = false;
    [SerializeField] private Vector2 ceilingCheckSize;

    public RaycastHit2D CeilingHit { get; private set; }
    public RaycastHit2D GroundHit { get; private set; }
    public RaycastHit2D RightWallHit { get; private set; }
    public RaycastHit2D LeftWallHit { get; private set; }

    private RaycastHit2D[] groundHits;
    private RaycastHit2D[] ceilingHits;
    private RaycastHit2D[] rightWallHits;
    private RaycastHit2D[] leftWallHits;
    private Transform Transform;
    private Vector2 groundCheckColliderSize;
    private Vector2 wallCheckColliderSize;
    private Vector2 ceilingCheckColliderSize;
    private RaycastHit2D lastGroundHit;
    private RaycastHit2D lastCeilingHit;
    private RaycastHit2D lastRightWallHit;
    private RaycastHit2D lastLeftWallHit;


    private void Awake()
    {
        Transform = transform;

        if (capsuleCollider == null)
        {
            Debug.LogError($"[{name}] Capsule collider is null, {name} disabled");
            enabled = false;
            return;
        }

        groundCheckColliderSize = new Vector2(groundCheckSize.x, capsuleCollider.size.y);
        wallCheckColliderSize = new Vector2(capsuleCollider.size.x, wallCheckSize.y);
        ceilingCheckColliderSize = new Vector2(ceilingCheckSize.x, capsuleCollider.size.y);
    }

    private void FixedUpdate()
    {
        PerformCollisionChecks();
        GatherCollisionsValues();
        CheckCollisionChanged();
    }

    private void PerformCollisionChecks()
    {
        if (checkGround)
            groundHits = GetCapsuleCastHits(groundCheckColliderSize, groundCheckSize.y, Vector2.down);

        if (checkCeiling)
            ceilingHits = GetCapsuleCastHits(ceilingCheckColliderSize, ceilingCheckSize.y, Vector2.up);

        if (checkRightWall)
            rightWallHits = GetCapsuleCastHits(wallCheckColliderSize, wallCheckSize.x, Vector2.left);

        if (checkLeftWall)
            leftWallHits = GetCapsuleCastHits(wallCheckColliderSize, wallCheckSize.x, Vector2.right);
    }

    private void GatherCollisionsValues()
    {
        if (checkGround)
            GroundHit = GetFirstValidHit(groundHits);

        if (checkCeiling)
            CeilingHit = GetFirstValidHit(ceilingHits);

        if (checkRightWall)
            RightWallHit = GetFirstValidHit(rightWallHits);

        if (checkLeftWall)
            LeftWallHit = GetFirstValidHit(leftWallHits);
    }

    private void CheckCollisionChanged()
    {
        if (checkGround && lastGroundHit != GroundHit)
        {
            lastGroundHit = GroundHit;
            OnGroundHitChanged?.Invoke(GroundHit);
        }

        if (checkCeiling && lastCeilingHit != CeilingHit)
        {
            lastCeilingHit = CeilingHit;
            OnCeilingHitChanged?.Invoke(CeilingHit);
        }

        if (checkRightWall && lastRightWallHit != RightWallHit)
        {
            lastRightWallHit = RightWallHit;
            OnRightWallHitChanged?.Invoke(RightWallHit);
        }

        if (checkLeftWall && lastLeftWallHit != LeftWallHit)
        {
            lastLeftWallHit = LeftWallHit;
            OnLeftWallHitChanged?.Invoke(LeftWallHit);
        }
    }

    private RaycastHit2D[] GetCapsuleCastHits(Vector2 checkSize, float distance, Vector2 direction) => Physics2D.CapsuleCastAll(capsuleCollider.bounds.center,
            checkSize,
            capsuleCollider.direction,
            Transform.eulerAngles.z,
            direction,
            distance,
            contactLayerMask);

    private RaycastHit2D GetFirstValidHit(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null && !Physics2D.GetIgnoreCollision(capsuleCollider, hit.collider))
                return hit;
        }

        return default; // returns an empty RaycastHit2D
    }
}
