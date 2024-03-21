using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameEndState : GameStateBase
{
    private int _stateId = 4;

    public GameEndState(GameStateManager gameStateManager, StateMachine stateMachine) : base(gameStateManager, stateMachine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Game ended!");
        base.EnterState();

        NetworkUIInput[] inputs = Object.FindObjectsOfType<NetworkUIInput>();

        foreach (var input in inputs)
        {
            input.OnEndGame();
        }

        UIManager.Instance.EndGameCanvas.gameObject.SetActive(true);
        UIManager.Instance.PlayerControllCanvas.gameObject.SetActive(false);
        UIManager.Instance.GameUICanvas.gameObject.SetActive(false);
        UIManager.Instance.GameStartCanvas.gameObject.SetActive(false);
        _gameStateManager.SendRpcStateId(_stateId);
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
