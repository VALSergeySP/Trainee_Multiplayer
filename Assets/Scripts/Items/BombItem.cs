using UnityEngine;

public class BombItem : CollectibleItemBase
{
    [SerializeField] private int _damageAmount = 50;
    [SerializeField] private float _damageRadius = 5f;
    [SerializeField] private float _despawnTime = 0.3f;
    [SerializeField] private BombProjectile _projectilePrefab;

    protected override void OnCollected(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BlowUp();
        }
    }

    private void BlowUp()
    {
        Runner.Spawn(_projectilePrefab.gameObject, transform.position, Quaternion.identity, null, (runner, o) =>
        {
            o.GetComponent<BombProjectile>().Init(_damageAmount, _damageRadius, _despawnTime);
        });
        Runner.Despawn(Object);
    }
}
