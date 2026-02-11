using UnityEngine;

public class JumpState : State
{
    [Header("State Data")]
    [SerializeField] private JumpStateDataSO stateData;

    [Header("State References")]
    [SerializeField] private SurfaceContactSensor surfaceContactSensor;
    [SerializeField] private JumpRiseState riseState;
    [SerializeField] private JumpApexState apexState;

    public float EarlyStopGravity => stateData.JumpEarlyStopGravity;

    private float bufferJumpInputTimePressed;
    private float coyoteLeftGroundTime;
    private bool isGrounded;

    protected override void Awake()
    {
        base.Awake();
        surfaceContactSensor.OnGroundHitChanged += OnGroundHitChanged;

        AddState(riseState).AddState(apexState);
    }

    protected override void OnEnable()
    {
        coyoteLeftGroundTime = 0.0f;
        bufferJumpInputTimePressed = 0.0f;

        SetCurrentState(riseState);

        riseState.OnCompleted += RiseState_OnCompleted;
        apexState.OnCompleted += ApexState_OnCompleted;

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        riseState.OnCompleted -= RiseState_OnCompleted;
        apexState.OnCompleted -= ApexState_OnCompleted;
    }

    private void OnDestroy()
    {
        surfaceContactSensor.OnGroundHitChanged -= OnGroundHitChanged;
    }

    #region Events

    private void OnGroundHitChanged(RaycastHit2D hit)
    {
        var isGrounded = hit.collider != null;

        // Just left the ground and it is not jumping (!enabled)
        if (!enabled && this.isGrounded && !isGrounded)
        {
            coyoteLeftGroundTime = Time.time;
        }

        this.isGrounded = isGrounded;
    }

    private void RiseState_OnCompleted()
    {
        if (apexState.ApexTime > 0)
            SetCurrentState(apexState);
        else
            SetStateComplete();
    }

    private void ApexState_OnCompleted() => SetStateComplete();

    #endregion

    // Can only perform normal jump if grounded
    public bool CanJump() => !enabled && isGrounded;

    // Can only perform coyote jump if not currently jumping (!enabled),
    // and jump input was pressed within the coyote time
    public bool CanCoyoteJump() => !enabled && Time.time - coyoteLeftGroundTime <= stateData.JumpCoyoteTime;

    // Can only perform buffer jump if not currently jumping (!enabled),
    // is grounded, and jump input was pressed within the buffer time
    public bool CanBufferJump() => !enabled && CanJump() && Time.time - bufferJumpInputTimePressed <= stateData.JumpBufferTime;

    public void OnJumpInputPressed() => bufferJumpInputTimePressed = Time.time;

    // Cancel jump Early if jump input is released
    public void OnJumpInputReleased()
    {
        if (enabled) // Only stop jump early if currently jumping
            SetStateComplete();
    }
}