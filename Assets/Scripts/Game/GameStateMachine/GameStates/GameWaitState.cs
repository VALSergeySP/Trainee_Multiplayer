using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaitState : GameStateBase
{
    private const int STATE_ID = 3;

    private string _stateName = "Waiting...";
    private float _timer;

    public GameWaitState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _timer = 0f;

        if(_gameStateManager.CheckIsGameEnd())
        {
            _gameStateManager.GameManagerStateMachine.ChangeState(_gameStateManager.EndState);
        } else
        {
            _gameStateManager.UIManagerInstance.Timer.StartTimerWithValue(_gameStateManager.WaitTime);
            _gameStateManager.UIManagerInstance.Timer.SetStateName(_stateName);
            _gameStateManager.SendRpcStateId(STATE_ID);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_timer >= _gameStateManager.WaitTime)
        {
            _gameStateManager.GameManagerStateMachine.ChangeState(_gameStateManager.WaveState);
        }
 
        _timer += Time.deltaTime;
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
