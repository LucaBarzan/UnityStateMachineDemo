using UnityEngine;

public class GroundedState : State
{
    [SerializeField] private GroundedMovementState idleState;
    [SerializeField] private WalkState walkState;
    [SerializeField] private SlopeState slopeState;
    [SerializeField] private GroundDataHandler groundData;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    private Vector2 movementDirection => movementDirectionProvider.MoveDirection;

    protected override void Awake()
    {
        base.Awake();

        AddState(walkState)
            .AddState(idleState)
            .AddState(slopeState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SelectState();
    }

    private void Update()
    {
        if (!groundData.IsGrounded)
        {
            SetStateComplete();
            return;
        }

        SelectState();
    }

    private void SelectState()
    {
        if (groundData.IsOnSlope)
        {
            SetCurrentState(slopeState);
            return;
        }

        if (movementDirection == Vector2.zero)
        {
            SetCurrentState(idleState);
            return;
        }

        SetCurrentState(walkState);
    }
}
