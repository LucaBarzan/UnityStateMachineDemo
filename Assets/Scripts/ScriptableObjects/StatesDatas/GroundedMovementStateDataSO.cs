using UnityEngine;

[CreateAssetMenu(fileName = "GroundedMovementStateData", menuName = "Scriptable Objects/State Data/Grounded Movement State Data")]
public class GroundedMovementStateDataSO : ScriptableObject
{
    [Header("Movement Settings")]
    [Tooltip("Target horizontal speed when moving")]
    public float TargetSpeed = 7.5f;
    
    [Tooltip("How quickly the character accelerates to target speed")]
    public float Acceleration = 100.0f;
    
    [Header("Gravity Settings")]
    [Tooltip("Gravity force applied to keep character grounded")]
    public float Gravity = 0.5f;
}
