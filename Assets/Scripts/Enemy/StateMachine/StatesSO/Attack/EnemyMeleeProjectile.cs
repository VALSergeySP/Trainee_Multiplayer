using Fusion;
using UnityEngine;

public class EnemyMeleeProjectile : NetworkBehaviour
{
    [SerializeField] private int _bulletDamage;
    public int Damage { get => _bulletDamage; }

    [Networked] private TickTimer life { get; set; }

    public void Init(float despawnTime)
    {
        life = TickTimer.CreateFromSeconds(Runner, despawnTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamagable>().Damage(_bulletDamage);
            Runner.Despawn(Object);
        }
    }
}
