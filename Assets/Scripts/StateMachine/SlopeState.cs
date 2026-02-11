using UnityEngine;

public class SlopeState : GroundedMovementState
{
    protected override float TargetHorizontalSpeed() => stateData.TargetSpeed * groundData.SlideHorizontalDirection;
}