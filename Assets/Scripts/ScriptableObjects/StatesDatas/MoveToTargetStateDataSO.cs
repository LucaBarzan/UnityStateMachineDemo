using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTargetStateData", menuName = "Scriptable Objects/State Data/Move To Target State Data")]
public class MoveToTargetStateDataSO : ScriptableObject
{
    [Header("Target Settings")]
    [Tooltip("Distance from target at which the state is considered complete")]
    public float ReachDistance = 0.1f;
}
