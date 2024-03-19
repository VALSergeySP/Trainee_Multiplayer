using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Distance Attack", menuName = "Enemy/Attack/Distance Attack")]
public class EnemyDistanceAttack : EnemyAttackSOBase
{
    [SerializeField] private float _timeBetweenAttacks = 1f;
    //[SerializeField] private Projectile _projectilePrefab;

    private float _timer;


    public override void DoAnimationTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenAttacks)
        {
            _timer = 0f;

            Debug.Log("Distance Attack!");
        }

        _timer += Time.deltaTime;
    }

    public override void DoPhisicsUpdateLogic()
    {
        base.DoPhisicsUpdateLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}