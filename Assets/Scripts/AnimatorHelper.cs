using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelper : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private readonly Dictionary<string, int> animationsIds = new Dictionary<string, int>();

    private bool TryGetAnimationId(string animationName, out int stateHash)
    {
        if (animationsIds.TryGetValue(animationName, out stateHash))
            return true;

        // Convert the state name to a hash for efficient comparison
        stateHash = Animator.StringToHash(animationName);

        // Iterate through all layers in the Animator Controller
        for (int i = 0; i < animator.layerCount; i++)
        {
            // Only succeed after confirming that this animator has this state
            if (animator.HasState(i, stateHash))
            {
                animationsIds.Add(animationName, stateHash);
                return true;
            }
        }

        stateHash = -1;
        return false;
    }

    private bool TryGetAnimationId(AnimationClip animation, out int stateHash) => TryGetAnimationId(animation.name, out stateHash);

    public void PlayAnim(AnimationClip animation)
    {
        if (TryGetAnimationId(animation, out var stateHash))
            animator.Play(stateHash, -1, 0);
    }
}