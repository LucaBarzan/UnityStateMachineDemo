using UnityEngine;

public class AttackState : State
{
    [Header("State Data")]
    [SerializeField] private AttackStateDataSO stateData;

    [Header("State References")]
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private GameObject attackObject;

    private float attackTimer;
    private float antecipationTimer;
    private AttackSubState subState;
    private enum AttackSubState
    {
        antecipation,
        attacked
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        movementDirectionProvider.Set(Vector2.zero);
        antecipationTimer = Time.time + stateData.AnticipationTime;
        subState = AttackSubState.antecipation;
    }

    private void Update()
    {
        switch (subState)
        {
            case AttackSubState.antecipation:
                if (Time.time > antecipationTimer)
                {
                    attackTimer = Time.time + stateData.AttackTime;
                    attackObject.SetActive(true);
                    subState = AttackSubState.attacked;
                }
                break;

            case AttackSubState.attacked:
                if (Time.time > attackTimer)
                {
                    SetStateComplete();
                }
                break;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        attackObject.SetActive(false);
    }
}