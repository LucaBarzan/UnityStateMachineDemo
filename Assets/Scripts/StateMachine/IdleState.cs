using UnityEngine;

public class IdleState : State
{
    [Header("State Data")]
    [SerializeField] private IdleStateDataSO stateData;

    [Header("State References")]
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;

    protected override void OnEnable()
    {
        base.OnEnable();
        movementDirectionProvider.Set(Vector2.zero);
        
        if (stateData.UseTimer)
            Invoke(nameof(SetStateComplete), stateData.IdleTime);
    }

    private void Update()
    {
        if (!stateData.UseTimer || movementDirectionProvider.MoveDirection != Vector2.zero)
            SetStateComplete();
    }
}