using UnityEngine;

public class GroundedMovementState : State
{
    [Header("State Data")]
    [SerializeField] protected GroundedMovementStateDataSO stateData;

    [Header("State References")]
    [SerializeField] protected PhysicsController2D physicsController2D;
    [SerializeField] protected GroundDataHandler groundData;

    protected float horizontalSpeed;
    protected Vector2 velocity;

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
            TargetHorizontalSpeed(),
            stateData.Acceleration * Time.deltaTime);

        // Align velocity with ground/slope direction
        velocity = horizontalSpeed * groundData.GroundDirection;
    }

    private void ApplyGroundGravity()
    {
        // Small downward push to keep character grounded
        velocity -= groundData.GroundNormal * stateData.Gravity;
    }

    protected virtual float TargetHorizontalSpeed() => stateData.TargetSpeed;
}