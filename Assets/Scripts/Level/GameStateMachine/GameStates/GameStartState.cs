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

        UIManager.Instance.PlayerControllCanvas.gameObject.SetActive(true);
        UIManager.Instance.GameUICanvas.gameObject.SetActive(true);
        UIManager.Instance.GameStartCanvas.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        base.ExitState();

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
