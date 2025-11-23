using UnityEngine;

public class AirborneState : State
{
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    [SerializeField] protected PhysicsController2D physicsController2D;
    [SerializeField] private PlayerMovementInput playerMovementInput;
    protected Vector2 velocity;
    private Vector2 movementDirection => playerMovementInput.InputDirection;
    private float horizontalSpeed = 0.0f;

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

    protected virtual void Update()
    {
        HandleDirection();
        HandleGravity();
        physicsController2D.SetVelocity(velocity);
    }

    public override void Exit()
    {
        base.Exit();

    }

    private void HandleDirection()
    {
        float horizontalInput = movementDirection.x;

        if (horizontalInput == 0)
        {
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, deceleration * Time.fixedDeltaTime);
        }
        // Is grounded or on the air
        else
        {
            float horizontalTargetSpeed = maxHorizontalSpeed * horizontalInput;

            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, horizontalTargetSpeed, acceleration * Time.fixedDeltaTime);
        }

        velocity.x = horizontalSpeed;
    }

    private void HandleGravity()
    {
        velocity.y = Mathf.MoveTowards(velocity.y, -maxFallSpeed, gravity * Time.deltaTime);
    }
}
