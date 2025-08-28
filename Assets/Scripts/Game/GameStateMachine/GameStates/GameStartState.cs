public class GameStartState : GameStateBase
{
    private const int STATE_ID = 1;

    public GameStartState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _gameStateManager.UIManagerInstance.GameStartCanvas.gameObject.SetActive(true);

        _gameStateManager.SendRpcStateId(STATE_ID); 
    }

    public override void ExitState()
    {
        base.ExitState();

        if (_gameStateManager.CanSpawnObjects())
        {
            _gameStateManager.EnemiesSpawnerInstance.Init();
        }

        _gameStateManager.UIManagerInstance.PlayerControllCanvas.gameObject.SetActive(true);
        _gameStateManager.UIManagerInstance.GameUICanvas.gameObject.SetActive(true);
        _gameStateManager.UIManagerInstance.GameStartCanvas.gameObject.SetActive(false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
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
