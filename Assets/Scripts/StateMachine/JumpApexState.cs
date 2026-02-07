using UnityEngine;

public class JumpApexState : AirborneState
{
    public float ApexTime => apexTime;

    [Tooltip("The time the player will be in the apex state, in seconds.\nWhen 0 this State is completely ignored")]
    [SerializeField] private float apexTime = 0.0f;

    protected override void OnEnable()
    {
        // Keep horizontal velocity and remove vertical velocity
        physicsController2D.SetVelocity(new Vector2(physicsController2D.Velocity.x, 0));
        base.OnEnable();
    }

    protected override bool IsStateComplete()=> TimePassed >= ApexTime;
}