using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaveState : GameStateBase
{
    private WaveSO _currentWave;

    public GameWaveState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Game wave state!");

        _currentWave = _gameStateManager.GetNextWave();

        if(_currentWave != null )
        {
            _currentWave.Init(_gameStateManager);
        } else
        {
            _gameStateManager.GameManagerStateMachine.ChangeState(_gameStateManager.EndState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Game wave exit state!");
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_currentWave != null)
        {
            _currentWave.DoFrameUpdateLogic();
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
