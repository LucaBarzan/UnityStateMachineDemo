using UnityEngine;

public class JumpApexState : BaseAirborneState<JumpApexStateDataSO>
{
    public float ApexTime => stateData.ApexTime;

    protected override void OnEnable()
    {
        // Keep horizontal velocity and remove vertical velocity
        physicsController2D.SetVelocity(new Vector2(physicsController2D.Velocity.x, 0));
        base.OnEnable();
    }

    protected override bool IsStateComplete()=> TimePassed >= ApexTime;
}