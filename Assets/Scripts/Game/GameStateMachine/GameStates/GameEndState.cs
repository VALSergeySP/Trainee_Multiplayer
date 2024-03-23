using UnityEngine;

public class GameEndState : GameStateBase
{
    private const int STATE_ID = 4;

    public GameEndState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        NetworkUIInput[] inputs = Object.FindObjectsOfType<NetworkUIInput>();

        foreach (var input in inputs)
        {
            input.OnEndGame();
        }

        _gameStateManager.UIManagerInstance.EndGameCanvas.gameObject.SetActive(true);
        _gameStateManager.UIManagerInstance.PlayerControllCanvas.gameObject.SetActive(false);
        _gameStateManager.UIManagerInstance.GameUICanvas.gameObject.SetActive(false);
        _gameStateManager.UIManagerInstance.GameStartCanvas.gameObject.SetActive(false);

        _gameStateManager.SendRpcStateId(STATE_ID);
    }

    public override void ExitState()
    {
        base.ExitState();
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
