using UnityEngine;

public class CharacterIdleState : GroundedMovementState
{
    [SerializeField] private float deceleration;

    protected override float TargetHorizontalSpeed => 0f;
    protected override float Acceleration => deceleration;
}
