using UnityEngine;

public class JumpApexState : BaseAirborneState<JumpApexStateDataSO>
{
    public float ApexTime => stateData.ApexTime;

    public override void EnterState()
    {
        // Keep horizontal velocity and remove vertical velocity
        physicsController2D.SetVelocity(new Vector2(physicsController2D.Velocity.x, 0));
        base.EnterState();
    }

    protected override bool IsStateComplete()=> TimePassed >= ApexTime;
}