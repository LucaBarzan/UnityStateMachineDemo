using UnityEngine;

public class WalkState : GroundedMovementState
{
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    private Vector2 movementDirection => movementDirectionProvider.MoveDirection;
    protected override float TargetHorizontalSpeed => maxSpeed * movementDirection.x;
    protected override float Acceleration => acceleration;

}
