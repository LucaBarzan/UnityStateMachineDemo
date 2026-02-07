#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;

public class StateMachineBehaviourDebugger : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public StateMachineBehaviour StateMachineBehaviour;

    private List<State> statesBranch = new List<State>();

    private void Awake()
    {
        Transform = transform;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || StateMachineBehaviour == null)
            return;

        statesBranch = StateMachineBehaviour.GetActiveStateBranch();
        var stateMachineHierarchy = string.Join(" -> ", statesBranch.ConvertAll(state => state.name));
        UnityEditor.Handles.Label(Transform.position, stateMachineHierarchy);
    }
}
#endif