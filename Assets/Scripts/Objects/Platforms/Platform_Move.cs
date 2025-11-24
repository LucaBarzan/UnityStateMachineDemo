using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float delayBetweenPoints = 0.0f;

    [SerializeField] private MoveTowards moveTowards;
    private int currentPointIndex = 0;

    private void Awake() => MoveToTargetPoint();

    public void OnReachedTarget()
    {
        currentPointIndex++;
        currentPointIndex %= movePoints.Length;

        Invoke(nameof(MoveToTargetPoint), delayBetweenPoints);
    }

    private void MoveToTargetPoint() => moveTowards.SetTarget(movePoints[currentPointIndex]);
}
