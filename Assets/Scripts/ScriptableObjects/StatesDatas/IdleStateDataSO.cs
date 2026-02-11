using UnityEngine;

[CreateAssetMenu(fileName = "IdleStateData", menuName = "Scriptable Objects/State Data/Idle State Data")]
public class IdleStateDataSO : ScriptableObject
{
    [Header("Idle Settings")]
    [Tooltip("Whether to use a timer to automatically exit idle state")]
    public bool UseTimer = false;
    
    [Tooltip("Time in seconds before automatically exiting idle state")]
    public float IdleTime = 0.0f;
}
