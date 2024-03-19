public abstract class State
{
    protected StateMachine _stateMachine;

    public State(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void NetworkUpdate() { }
}
