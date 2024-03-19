using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyIdleBaseInstance.DoAnimationTriggerLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.EnemyIdleBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.EnemyIdleBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(enemy.IsWithinAttackDistance)
        {
            enemy.StateMachineInstance.ChangeState(enemy.AttackState);
        }

        enemy.EnemyIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void NetworkUpdate()
    {
        base.NetworkUpdate();

        //enemy.EnemyIdleBaseInstance.DoNetworkUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyIdleBaseInstance.DoPhisicsUpdateLogic();
    }
}
