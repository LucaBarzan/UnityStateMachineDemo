using UnityEngine;

public class WalkState : GroundedMovementState
{
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;

    protected override float TargetHorizontalSpeed() => stateData.TargetSpeed * movementDirectionProvider.MoveDirection.x;
}
