using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine) : base(stateMachine)
    {
        this.enemy = enemy;
    }

    public override void EnterState()
    {
        base.EnterState();
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

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {

    }
}
