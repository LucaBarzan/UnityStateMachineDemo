using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Detects surface contacts (ground, walls, and ceiling) using BoxCast queries.
/// Attach this to a GameObject with a BoxCollider2D to sense nearby surfaces.
/// </summary>
public class SurfaceContactSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask contactLayerMask;

    [Header("Ground Detection")]
    [SerializeField] private bool checkGround = true;
    [SerializeField] private Vector2 groundCheckSize;

    [Header("Wall Detection")]
    [SerializeField] private bool checkWalls = true;
    [SerializeField] private Vector2 wallCheckSize;

    [Header("Ceiling Detection")]
    [SerializeField] private bool checkCeiling = true;
    [SerializeField] private Vector2 ceilingCheckSize;

    public RaycastHit2D CeilingHit { get; private set; }
    public RaycastHit2D GroundHit { get; private set; }
    public RaycastHit2D WallHit { get; private set; }

    private RaycastHit2D[] groundHits;
    private RaycastHit2D[] ceilingHits;
    private List<RaycastHit2D> wallHits = new List<RaycastHit2D>();
    private Transform Transform;
    private Vector2 groundCheckColliderSize;
    private Vector2 wallCheckColliderSize;
    private Vector2 ceilingCheckColliderSize;


    private void Awake()
    {
        Transform = transform;

        if (boxCollider == null)
        {
            Debug.LogError($"[{name}] Box collider is null, {name} disabled");
            enabled = false;
            return;
        }

        groundCheckColliderSize = new Vector2(groundCheckSize.x, boxCollider.size.y);
        wallCheckColliderSize = new Vector2(boxCollider.size.x, wallCheckSize.y);
        ceilingCheckColliderSize = new Vector2(ceilingCheckSize.x, boxCollider.size.y);
    }

    private void FixedUpdate()
    {
        PerformCollisionChecks();
        GatherCollisionsValues();
    }

    private void PerformCollisionChecks()
    {
        if (checkGround)
            groundHits = GetBoxCastHits(groundCheckColliderSize, groundCheckSize.y, Vector2.down);

        if (checkCeiling)
            ceilingHits = GetBoxCastHits(ceilingCheckColliderSize, ceilingCheckSize.y, Vector2.up);

        if (checkWalls)
        {
            wallHits.Clear();
            wallHits.AddRange(GetBoxCastHits(wallCheckColliderSize, wallCheckSize.x, Vector2.left));
            wallHits.AddRange(GetBoxCastHits(wallCheckColliderSize, wallCheckSize.x, Vector2.right));
        }
    }

    private void GatherCollisionsValues()
    {
        GroundHit = GetFirstValidHit(groundHits);
        CeilingHit = GetFirstValidHit(ceilingHits);
        WallHit = GetFirstValidHit(wallHits.ToArray());
    }

    private RaycastHit2D[] GetBoxCastHits(Vector2 checkSize, float distance, Vector2 direction) => Physics2D.BoxCastAll(boxCollider.bounds.center,
            checkSize,
            Transform.eulerAngles.z,
            direction,
            distance,
            contactLayerMask);

    private RaycastHit2D GetFirstValidHit(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null && !Physics2D.GetIgnoreCollision(boxCollider, hit.collider))
                return hit;
        }

        return default; // returns an empty RaycastHit2D
    }

    private void DrawCheck(Vector2 offset, Vector2 size, Bounds colliderBounds)
    {
        Vector2 center = (Vector2)colliderBounds.center + offset;
        Gizmos.DrawWireCube(center, size);
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.green;

        var bounds = boxCollider.bounds;
        var halfSize = boxCollider.size * 0.5f;

        if (checkGround)
        {
            DrawCheck(new Vector2(0f, -halfSize.y - groundCheckSize.y * 0.5f), groundCheckSize, bounds);
        }

        if (checkCeiling)
        {
            DrawCheck(new Vector2(0f, +halfSize.y + ceilingCheckSize.y * 0.5f), ceilingCheckSize, bounds);
        }

        if (checkWalls)
        {
            float horizontalOffset = halfSize.x + wallCheckSize.x * 0.5f;

            // Right wall
            DrawCheck(new Vector2(horizontalOffset, 0f), wallCheckSize, bounds);

            // Left wall
            DrawCheck(new Vector2(-horizontalOffset, 0f), wallCheckSize, bounds);
        }
    }
}
