using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Idle-Follow Player", menuName = "Enemy/Idle/Follow Player")]
public class EnemyFollowPlayer : EnemyIdleSOBase
{
    [SerializeField] private float _enemyMovementSpeed = 5f;

    private Transform _playerTransform;
    private Vector3 _movementDirection;

    public override void DoAnimationTriggerLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        _playerTransform = GetPlayerPosition();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _movementDirection = (_playerTransform.position - _enemy.transform.position).normalized;

        _enemy.MoveEnemy(_movementDirection * _enemyMovementSpeed);
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

    private Transform GetPlayerPosition()
    {
        if(_playersTransform.Count == 0) { return _enemy.transform; }

        int num = Random.Range(0, _playersTransform.Count);

        return _playersTransform[num];
    }
}
