using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : StateMachine
{
    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    public override void Initialize(State startingState)
    {
        base.Initialize(startingState);
        
    }
}
