using UnityEngine;

public class MoveToTargetState : State
{
    [Header("State Data")]
    [SerializeField] private MoveToTargetStateDataSO stateData;

    [Header("State References")]
    public Transform TargetTransform;
    [SerializeField] private Transform myTransform;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    
    private Vector2 lastPosition;
    private float squaredReachDistance;

    protected override void OnEnable()
    {
        base.OnEnable();
        movementDirectionProvider.Set(GetTargetDirection());
        squaredReachDistance = stateData.ReachDistance * stateData.ReachDistance;
        lastPosition = myTransform.position;
    }

    private void Update()
    {
        if (ReachedOnTarget())
        {
            SetStateComplete();
            return;
        }

        movementDirectionProvider.Set(GetTargetDirection());
        lastPosition = myTransform.position;
    }

    private bool ReachedOnTarget()
    {
        if (TargetTransform == null)
            return true;
        
        var closestLastPosition = Helpers.ClosestPointOnLine(lastPosition, myTransform.position, TargetTransform.position);
        var closestLastSquaredDistance = (closestLastPosition - TargetTransform.position).sqrMagnitude;
        return closestLastSquaredDistance <= squaredReachDistance;
    }

    private Vector2 GetTargetDirection() => (TargetTransform.position - myTransform.position).normalized;
}