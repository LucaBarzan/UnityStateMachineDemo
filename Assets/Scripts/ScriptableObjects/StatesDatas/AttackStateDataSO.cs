using UnityEngine;

[CreateAssetMenu(fileName = "AttackStateData", menuName = "Scriptable Objects/State Data/Attack State Data")]
public class AttackStateDataSO : ScriptableObject
{
    [Header("Attack Timing")]
    [Tooltip("Time in seconds before the attack is executed (windup/anticipation phase)")]
    public float AnticipationTime = 0.3f;
    
    [Tooltip("Time in seconds the attack object remains active")]
    public float AttackTime = 0.5f;
}
