using UnityEngine;

[CreateAssetMenu(fileName = "JumpRiseStateData", menuName = "Scriptable Objects/State Data/Jump Rise State Data")]
public class JumpRiseStateDataSO : AirborneStateDataSO
{
    [Header("Jump Settings")]
    [Tooltip("Initial upward velocity applied when jumping")]
    public float JumpStrength = 10.0f;
}
