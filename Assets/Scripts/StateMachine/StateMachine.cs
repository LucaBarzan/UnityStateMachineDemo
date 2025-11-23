public sealed class StateMachine
{
    public State State { get; private set; }

    public void Set(State newState, bool forceReset = false)
    {
        if ((newState != State || forceReset) && newState.IsAvailable)
        {
            State?.Exit();
            State = newState;
            State?.Enter();
        }
    }
}