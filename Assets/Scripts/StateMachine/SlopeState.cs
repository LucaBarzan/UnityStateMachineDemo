using UnityEngine;

public class SlopeState : GroundedMovementState
{
    [SerializeField] private float maxSlideSpeed;
    [SerializeField] private float slideAcceleration;

    protected override float TargetHorizontalSpeed =>
        maxSlideSpeed * groundData.SlideHorizontalDirection;

    protected override float Acceleration => slideAcceleration;
}