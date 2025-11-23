using UnityEngine;

public class PhysicsController2D : MonoBehaviour
{
    public Vector2 Velocity => velocity;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector2 velocity;
    private Vector2 baseVelocity;

    private void FixedUpdate()
    {
        if (_rigidbody2D == null)
            return;

        // Base force cannot be accumulated to not increase over time
        var finalVelocity = velocity + baseVelocity;
        
        _rigidbody2D.linearVelocity = finalVelocity;

        baseVelocity = Vector2.zero;
    }

    /// <summary>
    /// Adds the specified velocity to the object based on the given velocity type.
    /// </summary>
    /// <param name="velocity">The velocity to be added, represented as a <see cref="Vector2"/>.</param>
    /// <param name="additionalVelocityType">Specifies the type of velocity to which the value will be added. Use <see
    /// cref="AdditionalVelocityType.Additional"/> to add to the additional velocity, or <see
    /// cref="AdditionalVelocityType.Base"/> to add to the base velocity.</param>
    public virtual void AddVelocity(Vector2 velocity, AdditionalVelocityType additionalVelocityType)
    {
        switch (additionalVelocityType)
        {
            default:
            case AdditionalVelocityType.Additional:
                this.velocity += velocity;

                break;
            case AdditionalVelocityType.Base:
                baseVelocity += velocity;
                break;
        }
    }

    // TODO
    // public abstract void ApplyImpulseForce(Vector2 force);

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }
}