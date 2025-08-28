public class GameLoadingState : GameStateBase
{
    private const int STATE_ID = 0;

    public GameLoadingState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _gameStateManager.UIManagerInstance.LoadingCanvas.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        _gameStateManager.UIManagerInstance.LoadingCanvas.gameObject.SetActive(false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(!_gameStateManager.IsLoading)
        {
            _gameStateManager.GameManagerStateMachine.ChangeState(_gameStateManager.StartState);
        }
    }

    public override void NetworkUpdate()
    {
        base.NetworkUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
