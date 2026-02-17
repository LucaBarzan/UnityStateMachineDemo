using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : StateMachineBehaviour
{
    // OnCompleted uses a C# Action for lightweight, fast invocation.
    // Suitable for state transitions where performance and precise execution order matter.
    public event Action OnCompleted;

    // OnEnter and OnExit use UnityEvent to allow inspector-based binding.
    // This improves decoupling and makes it easy to hook up visual/audio feedback
    // such as SFX, VFX, and animations without additional code.
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public float TimePassed => Time.time - startTime;
    public bool Running => enabled;

    protected float startTime;

    // Setup variables BEFORE disabling itself
    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    public virtual void EnterState()
    {
        enabled = true;
        OnEnter?.Invoke();

        startTime = Time.time;
    }

    public virtual void ExitState()
    {
        enabled = false;
        OnExit?.Invoke();
    }

    protected void SetStateComplete()
    {
        if (Running) // Only invoke the event if we are currently enabled
        {
            enabled = false; // We must disable before invoking the event
            OnCompleted?.Invoke();
        }
    }
}