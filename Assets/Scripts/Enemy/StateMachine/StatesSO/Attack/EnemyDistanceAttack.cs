using UnityEngine;

[CreateAssetMenu(fileName = "Distance Attack", menuName = "Enemy/Attack/Distance Attack")]
public class EnemyDistanceAttack : EnemyAttackSOBase
{
    [SerializeField] private float _timeBetweenAttacks = 1f;
    [SerializeField] private BulletProjectile _projectilePrefab;
    [SerializeField] private float _despawnTime = 3f;

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

            Shoot();
        }

        _timer += Time.deltaTime;
    }

    private void Shoot()
    {
        float dist = 0f;
        float newDist;
        Vector2 target = Vector2.zero;
        foreach (var player in _playersTransform)
        {
            newDist = Vector2.Distance(_transform.position, player.position);
            if (newDist < dist || dist == 0)
            {
                dist = newDist;
                target = player.position;
            }
        }
        Vector2 aimDirection = (target - (Vector2)_transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _enemy.Spawner.SpawnBulletInNetwork(_projectilePrefab, _transform.position, angle, _despawnTime);
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