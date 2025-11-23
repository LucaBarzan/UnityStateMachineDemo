using UnityEngine;

public class JumpState : AirborneState
{
    public override bool IsAvailable => surfaceContactSensor != null && surfaceContactSensor.GroundHit;

    [SerializeField] private SurfaceContactSensor surfaceContactSensor;
    [SerializeField] private float jumpStrength;

    public override void Enter()
    {
        IsComplete = false;
        physicsController2D.AddVelocity(Vector2.up * jumpStrength, AdditionalVelocityType.Additional);
        base.Enter();
    }

    protected override void Update()
    {
        base.Update();

        if (velocity.y <= 0)
            IsComplete = true;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
