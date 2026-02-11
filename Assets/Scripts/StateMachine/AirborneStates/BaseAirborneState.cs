using UnityEngine;

public abstract class BaseAirborneState<T> : State where T : AirborneStateDataSO
{
    [Header("State Data")]
    [SerializeField] protected T stateData;

    [Header("State References")]
    [SerializeField] protected PhysicsController2D physicsController2D;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] protected SurfaceContactSensor surfaceContactSensor;

    protected Vector2 velocity;
    private Vector2 movementDirection => movementDirectionProvider.MoveDirection;
    private float horizontalSpeed = 0.0f;
    private float gravity;

    protected override void Awake()
    {
        gravity = stateData.Gravity;

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
        ResetGravity();
        base.OnDisable();
    }

    private void HandleDirection()
    {
        float horizontalInput = movementDirection.x;

        if (horizontalInput == 0)
        {
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, stateData.Deceleration * Time.fixedDeltaTime);
        }
        // Is grounded or on the air
        else
        {
            float horizontalTargetSpeed = stateData.MaxHorizontalSpeed * horizontalInput;

            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, horizontalTargetSpeed, stateData.Acceleration * Time.fixedDeltaTime);
        }

        velocity.x = horizontalSpeed;
    }

    private void HandleGravity()
    {
        velocity.y = Mathf.MoveTowards(velocity.y, -stateData.MaxFallSpeed, gravity * Time.deltaTime);
    }

    protected virtual bool IsStateComplete() => surfaceContactSensor.GroundHit;

    public void SetGravity(float gravity) => this.gravity = gravity;

    public void ResetGravity() => gravity = stateData.Gravity;
}