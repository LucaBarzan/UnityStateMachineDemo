using UnityEngine;

[CreateAssetMenu(fileName = "JumpStateData", menuName = "Scriptable Objects/State Data/Jump State Data")]
public class JumpStateDataSO : ScriptableObject
{
    [Header("Jump Timing")]
    [Tooltip("Coyote time: Grace period after leaving a platform where jump is still allowed (in seconds)")]
    [Range(0f, 0.3f)]
    public float JumpCoyoteTime = 0.075f;
    
    [Tooltip("Jump buffer time: Window before landing where jump input is remembered (in seconds)")]
    [Range(0f, 0.3f)]
    public float JumpBufferTime = 0.15f;
    
    [Header("Jump Mechanics")]
    [Tooltip("Gravity multiplier applied when jump button is released early (for variable jump height)")]
    public float JumpEarlyStopGravity = 160f;
}
