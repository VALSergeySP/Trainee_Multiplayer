using UnityEngine;

public class GameWaveState : GameStateBase
{
    private const int STATE_ID = 2;

    private WaveSO _currentWave;

    public GameWaveState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _currentWave = _gameStateManager.GetNextWave();

        if (_gameStateManager.CanSpawnObjects())
        {
            if (_currentWave != null)
            {
                _currentWave.Init(_gameStateManager);
            }
            else
            {
                _gameStateManager.GameManagerStateMachine.ChangeState(_gameStateManager.EndState);
            }
        }

        if (_currentWave != null)
        {
            _gameStateManager.UIManagerInstance.Timer.StartTimerWithValue(_currentWave.WaveDurationTime);
            _gameStateManager.UIManagerInstance.Timer.SetStateName(_currentWave.WaveName);
        } else
        {
            _gameStateManager.UIManagerInstance.Timer.ResetTimer();
            _gameStateManager.UIManagerInstance.Timer.SetStateName("Wave...");
        }

        _gameStateManager.SendRpcStateId(STATE_ID);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (_gameStateManager.CanSpawnObjects())
        {
            if (_currentWave != null)
            {
                _currentWave.DoFrameUpdateLogic();
            }
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
