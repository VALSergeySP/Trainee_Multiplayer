using Fusion;
using UnityEngine;

public class EnemyMeleeProjectile : NetworkBehaviour
{
    [SerializeField] private int _bulletDamage;
    public int Damage { get => _bulletDamage; }

    private const string PLAYER_TAG = "Player";

    [Networked] private TickTimer Life { get; set; }

    public void Init(float despawnTime)
    {
        Life = TickTimer.CreateFromSeconds(Runner, despawnTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (Life.Expired(Runner))
            Runner.Despawn(Object);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            collision.GetComponent<IDamagable>().Damage(_bulletDamage);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
