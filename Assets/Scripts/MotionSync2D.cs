using UnityEngine;

public class MotionSync2D : MonoBehaviour
{
    [SerializeField] private PhysicsController2D physicsController2D;
    private Collider2D connectedCollider2D;
    private Transform connectedTransform;
    private Vector2 previousConnectionPosition;
    private Vector2 previousConnectionVelocity;
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    public void SetConnectedCollider(Collider2D other)
    {
        connectedCollider2D = other;
        connectedTransform = other.transform;
        previousConnectionPosition = connectedTransform.position;
    }

    public void RemoveConnectedCollider(Collider2D other)
    {
        var currentConnectionVelocity = GetConnectionVelocity();
        var dot = Vector2.Dot(previousConnectionVelocity, currentConnectionVelocity);
        var distance = (previousConnectionVelocity - currentConnectionVelocity).sqrMagnitude;

        // Debug.Log($"previous {previousConnectionVelocity} current {currentConnectionVelocity}");
        // Debug.Log($"dot {dot}");
        // Debug.Log($"distance {distance}");

        if (connectedCollider2D == other && distance > 0.1f)
        {
            // connectedCollider2D = null;
            // physicsController2D.AddVelocity(Vector3.zero, AdditionalVelocityType.Base);
        }
    }

    private void FixedUpdate()
    {
        if (connectedCollider2D != null)
        {
            var connectionVelocity = GetConnectionVelocity();
            physicsController2D.AddVelocity(connectionVelocity, AdditionalVelocityType.Base);
            previousConnectionVelocity = connectionVelocity;
            previousConnectionPosition = connectedTransform.position;
        }
    }

    private Vector2 GetConnectionVelocity()
    {
        var connectionMovement = (Vector2)connectedTransform.position - previousConnectionPosition;
        return connectionMovement / Time.fixedDeltaTime;
    }
}