using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : GameStateBase
{
    public GameStartState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Game start state!");

        UIManager.Instance.GameStartCanvas.gameObject.SetActive(true);

        _gameStateManager.SendRpcStateId(1); 
    }

    public override void ExitState()
    {
        base.ExitState();

        if (_gameStateManager.CanSpawnObjects())
        {
            EnemiesSpawner.Instance.Init();
        }
        UIManager.Instance.PlayerControllCanvas.gameObject.SetActive(true);
        UIManager.Instance.GameUICanvas.gameObject.SetActive(true);
        UIManager.Instance.GameStartCanvas.gameObject.SetActive(false);
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
