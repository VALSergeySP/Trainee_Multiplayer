using Fusion;
using UnityEngine;

public class BombProjectile : NetworkBehaviour
{
    private int _projectileDamage;

    [Networked] private TickTimer life { get; set; }

    public void Init(int damage, float radius, float despawnTime)
    {
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
            collision.GetComponent<IDamagable>().Damage(_projectileDamage);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
