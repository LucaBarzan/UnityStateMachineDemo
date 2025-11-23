using UnityEngine;

public class GroundedState : State 
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float gravity;
    [SerializeField] private float maxSlopeAngle = 45.0f;
    [SerializeField] private float notWalkableSlope_MaxSlideSpeed;
    [SerializeField] private float notWalkableSlope_SlideAcceleration;

    [SerializeField] private PhysicsController2D physicsController2D;
    [SerializeField] private SurfaceContactSensor surfaceContactSensor;
    [SerializeField] private PlayerMovementInput playerMovementInput;

    private Vector2 movementDirection => playerMovementInput.InputDirection;
    private RaycastHit2D groundHit => surfaceContactSensor.GroundHit;

    private float horizontalSpeed = 0.0f;
    private Vector2 velocity;

    // Slope related
    private float slopeAngle = 0.0f;
    private bool walkableGround = false;
    private float groundFlowSign = 1.0f;
    private Vector2 groundDirection = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        IsComplete = true;
    }

    public override void Enter()
    {
        base.Enter();
        velocity = physicsController2D.Velocity;
        horizontalSpeed = velocity.x;
    }

    private void Update()
    {
        EvaluateGroundSlope();
        HandleDirection();
        HandleGravity();
        physicsController2D.SetVelocity(velocity);
    }

    public override void Exit()
    {
        base.Exit();

    }

    private void EvaluateGroundSlope()
    {
        slopeAngle = Vector2.Angle(groundHit.normal, Vector2.up);

        walkableGround = slopeAngle <= maxSlopeAngle;

        groundDirection = -Vector2.Perpendicular(groundHit.normal).normalized;
        groundFlowSign = Mathf.Sign(Vector2.Dot(groundDirection, Vector2.down));
    }

    private void HandleDirection()
    {
        float horizontalInput = movementDirection.x;

        if (!walkableGround)
        {
            // Add reverse velocity to the player
            horizontalSpeed = Mathf.MoveTowards(
                horizontalSpeed,
                notWalkableSlope_MaxSlideSpeed * groundFlowSign,
                notWalkableSlope_SlideAcceleration * Time.deltaTime);
        }
        else if (horizontalInput == 0)
        {
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, deceleration * Time.deltaTime);
        }
        // Is grounded or on the air
        else
        {
            float horizontalTargetSpeed = maxSpeed * horizontalInput;

            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, horizontalTargetSpeed, acceleration * Time.deltaTime);
        }

        // Handle Slope horizontal movement
        velocity = horizontalSpeed * groundDirection;
    }

    private void HandleGravity()
    {
        // Add little gravity to stick to the ground
        velocity -= groundHit.normal * gravity;
    }
}
