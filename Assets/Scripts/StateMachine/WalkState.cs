using UnityEngine;

public class WalkState : GroundedMovementState
{
    [Header("Walk State References")]
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;

    protected override float TargetHorizontalSpeed() => targetSpeed * movementDirectionProvider.MoveDirection.x;
}
