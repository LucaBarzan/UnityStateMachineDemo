using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    public event Action OnCompleted;

    public float TimePassed => Time.time - startTime;

    protected float startTime;

    // Setup variables BEFORE disabling itself
    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    protected virtual void OnEnable() => startTime = Time.time;

    protected void SetStateComplete()
    {
        if (enabled) // Only invoke the event if we are currently enabled
        {
            enabled = false; // We must disable before invoking the event
            OnCompleted?.Invoke();
        }
    }
}