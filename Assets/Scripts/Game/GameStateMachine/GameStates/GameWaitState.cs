using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaitState : GameStateBase
{
    private string _stateName = "Waiting...";
    private float _timer;

    public GameWaitState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _timer = 0f;

        Debug.Log("Game wait state!");
        if(_gameStateManager.CheckIsGameEnd())
        {
            _gameStateManager.GameManagerStateMachine.ChangeState(_gameStateManager.EndState);
        } else
        {
            UIManager.Instance.Timer.StartTimerWithValue(_gameStateManager.WaitTime);
            UIManager.Instance.Timer.SetStateName(_stateName);
            _gameStateManager.SendRpcStateId(3);
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
