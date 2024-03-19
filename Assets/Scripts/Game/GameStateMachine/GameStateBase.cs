using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase : State
{
    protected GameStateManager _gameStateManager;

    public GameStateBase(GameStateManager gameStateManager, StateMachine stateMachine) : base(stateMachine)
    {
        this._gameStateManager = gameStateManager;
    }

    //public virtual void ChangeCanvas(Canvas canvas) { }
}
