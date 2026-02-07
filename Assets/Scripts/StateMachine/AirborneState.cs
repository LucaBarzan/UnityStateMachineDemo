using UnityEngine;

public class AirborneState : State
{
    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    [SerializeField] protected PhysicsController2D physicsController2D;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] protected SurfaceContactSensor surfaceContactSensor;

    protected Vector2 velocity;
    private Vector2 movementDirection => movementDirectionProvider.MoveDirection;
    private float horizontalSpeed = 0.0f;
    private float originalGravity;

    protected override void Awake()
    {
        originalGravity = gravity;

        base.Awake(); // Must be called after setting up variables, otherwise it will disable itself before we can setup variables
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        velocity = physicsController2D.Velocity;
        horizontalSpeed = velocity.x;
    }

    protected virtual void Update()
    {
        if (IsStateComplete())
        {
            SetStateComplete();
            return;
        }

        HandleDirection();
        HandleGravity();
        physicsController2D.SetVelocity(velocity);
    }

    protected override void OnDisable()
    {
        gravity = originalGravity;
        base.OnDisable();
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

    protected virtual bool IsStateComplete() => surfaceContactSensor.GroundHit;

    public void SetGravity(float gravity) => this.gravity = gravity;

    public void ResetGravity() => gravity = originalGravity;
}