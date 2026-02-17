using UnityEngine;

public class GroundDataHandler : MonoBehaviour
{
    // Ground Data
    public bool IsGrounded => groundHit;
    public Vector2 GroundDirection => groundDirection;
    public Vector2 GroundNormal => groundNormal;

    // Slide Data
    public Vector2 SlideDirection => slideDirection;
    public float SlideHorizontalDirection => slideHorizontalDirection;

    // Slope Data
    public bool IsOnSlope => isOnSlope;
    public float SlopeAngle => slopeAngle;

    [SerializeField] SurfaceContactSensor surfaceContactSensor;
    [SerializeField] private float maxSlopeAngle = 45.0f;

    private RaycastHit2D groundHit => surfaceContactSensor.GroundHit;

    // Ground Data
    private Vector2 groundDirection = Vector2.zero;
    private Vector2 groundNormal = Vector2.zero;

    // Slope data
    private float slopeAngle = 0.0f;
    private bool isOnSlope = false;

    // Slide data
    private Vector2 slideDirection = Vector2.zero;
    private float slideHorizontalDirection = 0.0f;

    private void FixedUpdate()
    {
        if (!surfaceContactSensor.GroundHit)
            return;

        // Handle ground data
        groundNormal = groundHit.normal;
        groundDirection = -Vector2.Perpendicular(groundNormal).normalized;

        // Handle Slope data
        slopeAngle = Vector2.Angle(groundHit.normal, Vector2.up);
        isOnSlope = slopeAngle > maxSlopeAngle;

        // Handle Slide data
        slideHorizontalDirection = Mathf.Sign(Vector2.Dot(groundDirection, Vector2.down));
        slideDirection = groundDirection * slideHorizontalDirection;
    }
}
