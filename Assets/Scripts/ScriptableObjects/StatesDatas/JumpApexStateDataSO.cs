using UnityEngine;

[CreateAssetMenu(fileName = "JumpApexStateData", menuName = "Scriptable Objects/State Data/Jump Apex State Data")]
public class JumpApexStateDataSO : AirborneStateDataSO
{
    [Header("Apex Settings")]
    [Tooltip("The time the player will be in the apex state, in seconds. When 0 this state is completely ignored")]
    public float ApexTime = 0.1f;
}
