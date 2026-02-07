using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBehaviour : MonoBehaviour
{
    public Transform Transform { get; private set; }

    public State State { get; private set; }

    private readonly HashSet<State> states = new();

    protected virtual void Awake()
    {
        Transform = transform;
    }

    protected virtual void OnDisable()
    {
        DisableAllStates();
    }

    public StateMachineBehaviour AddState(State state)
    {
        states.Add(state);
        return this;
    }

    public void SetCurrentState(State newState, bool forceReset = false)
    {
        if (ContainsState(newState) && (newState != State || forceReset))
        {
            if (State != null)
                State.enabled = false;

            State = newState;

            if (State != null)
                State.enabled = true;
        }
    }

    public void DisableAllStates()
    {
        foreach (var state in states)
            state.enabled = false;

        State = null;
    }

    private bool ContainsState(State state) => states != null && states.Count > 0 && states.Contains(state);

    public List<State> GetActiveStateBranch(List<State> states = null)
    {
        if (states == null)
            states = new List<State>();

        if (State == null || !State.enabled)
            return states;

        states.Add(State);
        return State.GetActiveStateBranch(states);
    }
}