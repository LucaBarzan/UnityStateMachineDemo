
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] WaypointPatrollerState patrolState;
    [SerializeField] MoveToTargetState chaseState;
    [SerializeField] AttackState attackState;

    protected override void Awake()
    {
        base.Awake();
        AddState(patrolState)
            .AddState(chaseState)
            .AddState(attackState);
    }

    private void OnEnable()
    {
        attackState.OnCompleted += AttackState_OnCompleted;
        Application.targetFrameRate = -1;
    }

    private void Start()
    {
        SetCurrentState(patrolState);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        attackState.OnCompleted -= AttackState_OnCompleted;
    }

    private void AttackState_OnCompleted()
    {
        if (chaseState.TargetTransform != null)
            SetCurrentState(chaseState);
        else
            SetCurrentState(patrolState);
    }

    public void OnPlayerInsideRangeAttack()
    {
        SetCurrentState(attackState);
    }

    public void OnPlayerDetected(Collider2D collider2D)
    {
        chaseState.TargetTransform = collider2D.transform;
        SetCurrentState(chaseState);
    }
}