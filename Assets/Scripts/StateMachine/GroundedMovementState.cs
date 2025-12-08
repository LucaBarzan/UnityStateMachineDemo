using UnityEngine;

public abstract class GroundedMovementState : State
{
    [Header("References")]
    [SerializeField] protected PhysicsController2D physicsController2D;
    [SerializeField] protected GroundDataHandler groundData;

    [Header("Forces")]
    [SerializeField] protected float gravity;

    protected float horizontalSpeed;
    protected Vector2 velocity;

    protected abstract float TargetHorizontalSpeed { get; }
    protected abstract float Acceleration { get; }

    protected override void OnEnable()
    {
        base.OnEnable();
        velocity = physicsController2D.Velocity;
        horizontalSpeed = velocity.x;
    }

    private void Update()
    {
        HandleHorizontal();
        ApplyGroundGravity();
        physicsController2D.SetVelocity(velocity);
    }

    private void HandleHorizontal()
    {
        horizontalSpeed = Mathf.MoveTowards(
            horizontalSpeed,
            TargetHorizontalSpeed,
            Acceleration * Time.deltaTime);

        // Align velocity with ground/slope direction
        velocity = horizontalSpeed * groundData.GroundDirection;
    }

    private void ApplyGroundGravity()
    {
        // Small downward push to keep character grounded
        velocity -= groundData.GroundNormal * gravity;
    }
}