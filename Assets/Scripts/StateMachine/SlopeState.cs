using UnityEngine;

public class SlopeState : GroundedMovementState
{
    protected override float TargetHorizontalSpeed() => targetSpeed * groundData.SlideHorizontalDirection;
}