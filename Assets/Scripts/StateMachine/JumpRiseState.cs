using UnityEngine;

public class JumpRiseState : AirborneState
{
    [SerializeField] private float jumpStrength;

    protected override void OnEnable()
    {
        // Keep horizontal velocity and apply jump strength to vertical velocity
        var jumpForce = new Vector2(physicsController2D.Velocity.x, jumpStrength);
        physicsController2D.SetVelocity(jumpForce);
        base.OnEnable();
    }

    protected override bool IsStateComplete() => velocity.y <= 0;
}
