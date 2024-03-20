using UnityEngine;

[CreateAssetMenu(fileName = "Mellee Attack", menuName = "Enemy/Attack/Mellee Attack")]
public class EnemyMeleeAttack : EnemyAttackSOBase
{
    [SerializeField] private float _timeBetweenAttacks = 1f;
    [SerializeField] private EnemyMeleeProjectile _attackPrefab;

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

        if(_timer > _timeBetweenAttacks)
        {
            _timer = 0f;

            Attack();
        }

        _timer += Time.deltaTime;
    }

    private void Attack()
    {
        EnemiesSpawner.Instance.SpawnMelleeInNetwork(_attackPrefab, _transform.position, 0.1f);
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
