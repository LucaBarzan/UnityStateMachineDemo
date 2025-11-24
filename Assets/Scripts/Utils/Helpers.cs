using UnityEngine;

public static class Helpers
{
    public static bool IsCloseTo(this Vector2 positionA, Vector2 positionB, float threshold = 0.1f)
    {
        float distanceSqr = (positionA - positionB).sqrMagnitude;
        return distanceSqr <= (threshold * threshold);
    }

}