public abstract class GameStateBase : State
{
    protected GameStateManager _gameStateManager;

    public GameStateBase(GameStateManager gameStateManager, StateMachine stateMachine) : base(stateMachine)
    {
        this._gameStateManager = gameStateManager;
    }
}
