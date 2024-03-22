using UnityEngine;
using Fusion;

public class BombItem : CollectibleItemBase
{
    [SerializeField] private int _damageAmount = 50;
    [SerializeField] private float _damageRadius = 5f;
    [SerializeField] private float _despawnTime = 0.3f;
    [SerializeField] private BombProjectile _projectilePrefab;

    private int _playerId = 0;

    protected override void OnCollected(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerId = collision.GetComponent<NetworkObject>().InputAuthority.PlayerId;
            BlowUp();
        }
    }

    private void BlowUp()
    {
        Runner.Spawn(_projectilePrefab.gameObject, transform.position, Quaternion.identity, null, (runner, o) =>
        {
            o.GetComponent<BombProjectile>().Init(_damageAmount, _damageRadius, _despawnTime, _playerId);
        });
        Runner.Despawn(Object);
    }
}
