using UnityEngine;

public class WaypointPatrollerState : State
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private MoveToTargetState moveToTargetState;
    [SerializeField] private IdleState idleState;

    private Transform waypointTransform => waypoints[currentWaypoint];

    private int currentWaypoint;

    protected override void Awake()
    {
        base.Awake();
        AddState(moveToTargetState).AddState(idleState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        moveToTargetState.OnCompleted += MoveToTargetState_OnCompleted;
        idleState.OnCompleted += IdleState_OnCompleted;

        moveToTargetState.TargetTransform = waypointTransform;
        SetCurrentState(moveToTargetState);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        moveToTargetState.OnCompleted -= MoveToTargetState_OnCompleted;
        idleState.OnCompleted -= IdleState_OnCompleted;
    }

    private void MoveToTargetState_OnCompleted() => SetCurrentState(idleState);

    private void IdleState_OnCompleted()
    {
        currentWaypoint++;
        currentWaypoint %= waypoints.Length;
        moveToTargetState.TargetTransform = waypointTransform;
        SetCurrentState(moveToTargetState);
    }
}
