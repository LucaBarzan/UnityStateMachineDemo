using UnityEngine;

public class ChaseState : MoveToTargetState
{
    [SerializeField] private GameObject attackRangeSensor;

    public override void EnterState()
    {
        base.EnterState();
        attackRangeSensor.SetActive(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        attackRangeSensor.SetActive(false);
    }
}
