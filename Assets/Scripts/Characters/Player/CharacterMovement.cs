using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : StateMachineBehaviour
{
    public Vector3 Position => Transform.position;

    [SerializeField] private SurfaceContactSensor contactSensor;
    [SerializeField] protected MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private GroundedState groundedState;
    [SerializeField] protected BaseAirborneState<AirborneStateDataSO> airborneState;

    private float horizontalDirection;

    protected override void Awake()
    {
        base.Awake();

        AddState(groundedState).AddState(airborneState);
    }

    protected virtual void OnEnable()
    {
        groundedState.OnCompleted += GroundedState_OnCompleted;
        airborneState.OnCompleted += AirborneState_OnCompleted;
    }

    private void AirborneState_OnCompleted() => SelectState();

    private void GroundedState_OnCompleted() => SelectState();

    protected virtual void Start() => SelectState();

    protected virtual void Update()
    {
        SetFacingDirection(movementDirectionProvider.MoveDirection.x);
    }

    protected virtual void SelectState()
    {
        if (contactSensor.GroundHit)
        {
            SetCurrentState(groundedState);
        }
        else
        {
            SetCurrentState(airborneState);
        }
    }

    private void SetFacingDirection(float horizontalDirection)
    {
        if (horizontalDirection == 0) // No horizontal input, keep current facing direction
            return;

        var facingDirection = Mathf.Sign(horizontalDirection);
        var horizontalScale = Mathf.Abs(Transform.localScale.x) * facingDirection;
        Transform.localScale = new Vector3(horizontalScale, Transform.localScale.y, Transform.localScale.z);
    }
}