using Fusion;
using UnityEngine;

public class BombProjectile : NetworkBehaviour
{
    private int _projectileDamage;
    private int _playerPickedItUp;

    [Networked] private TickTimer life { get; set; }

    public void Init(int damage, float radius, float despawnTime, int playerId)
    {
        _playerPickedItUp = playerId;
        transform.localScale = transform.localScale * radius;
        _projectileDamage = damage;
        life = TickTimer.CreateFromSeconds(Runner, despawnTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().Damage(_projectileDamage, _playerPickedItUp);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
