using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadingState : GameStateBase
{
    public GameLoadingState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        UIManager.Instance.LoadingCanvas.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        UIManager.Instance.LoadingCanvas.gameObject.SetActive(false);
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
