using UnityEngine;

public class JumpRiseState : BaseAirborneState<JumpRiseStateDataSO>
{
    protected override void OnEnable()
    {
        // Keep horizontal velocity and apply jump strength to vertical velocity
        var jumpForce = new Vector2(physicsController2D.Velocity.x, stateData.JumpStrength);
        physicsController2D.SetVelocity(jumpForce);
        base.OnEnable();
    }

    protected override bool IsStateComplete() => velocity.y <= 0;
}
