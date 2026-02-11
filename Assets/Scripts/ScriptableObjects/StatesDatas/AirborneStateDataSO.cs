using UnityEngine;

[CreateAssetMenu(fileName = "AirborneStateData", menuName = "Scriptable Objects/State Data/Airborne State Data")]
public class AirborneStateDataSO : ScriptableObject
{
    [Header("Gravity Settings")]
    [Tooltip("Gravity force applied while airborne")]
    public float Gravity = 80.0f;
    
    [Tooltip("Maximum falling speed")]
    public float MaxFallSpeed = 10.0f;
    
    [Header("Air Control")]
    [Tooltip("Maximum horizontal speed while in the air")]
    public float MaxHorizontalSpeed = 7.5f;
    
    [Tooltip("Acceleration rate for horizontal movement in air")]
    public float Acceleration = 70.0f;
    
    [Tooltip("Deceleration rate when no input is given")]
    public float Deceleration = 70.0f;
}
