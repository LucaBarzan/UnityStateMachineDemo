using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool IsComplete { get; protected set; }
    public virtual bool IsAvailable => true;

    private float startTime;

    public float TimePassed => Time.time - startTime;


    protected virtual void Awake() => enabled = false;

    public virtual void Enter()
    {
        startTime = Time.time;
        enabled = true;
    }

    public virtual void Exit() => enabled = false;
}
