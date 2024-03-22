using Fusion;
using UnityEngine;

public abstract class CollectibleItemBase : NetworkBehaviour
{
    [SerializeField] private float _itemDespawnTime = 20f;
    [Networked] private TickTimer life { get; set; }

    public override void Spawned()
    {
        life = TickTimer.CreateFromSeconds(Runner, _itemDespawnTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollected(collision);
    }

    protected abstract void OnCollected(Collider2D collision);
}
